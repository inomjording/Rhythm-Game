using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.CharacterSprite;
using RhythmGame.Collections;
using RhythmGame.DanceContext.BeatButtons;
using RhythmGame.DanceContext.Beats;
using RhythmGame.ScoreContext;

namespace RhythmGame.DanceContext;

public class DanceContext(
    ContentManager content,
    SoundEffectCollection soundEffects,
    string selectedSong,
    FontCollection fonts,
    ContextManager contextManager) : IGameContext
{
    private Texture2D arrowTexture;
    private Texture2D arrow;
    private Texture2D background;
    private Color backgroundTint = Color.White;
    private KeyboardState oldState;
    private ScoreManager scoreManager;

    public static readonly Vector2 Origin = new(200, 240);

    // Create beat buttons for each direction
    private BeatButtonUp upButton;
    private BeatButtonDown downButton;
    private BeatButtonLeft leftButton;
    private BeatButtonRight rightButton;


    private BeatManager beatManager;
    private List<Beat> activeBeats = [];

    // Audio objects
    private SoundEffect song;
    private SoundEffectInstance songInstance;

    // Character
    private DancingCharacter dancingCharacter;
    private const string CharacterSpriteFolder = "character-sprites/mc/";

    private readonly Stopwatch stopwatch = Stopwatch.StartNew();

    public bool ReturnToMainMenu { get; set; }

    public void LoadContent()
    {
        arrowTexture = content.Load<Texture2D>("character-sprites/arrows/active-arrow");
        arrow = content.Load<Texture2D>("character-sprites/arrows/full-arrow");
        background = content.Load<Texture2D>("bg2");
        scoreManager = new ScoreManager(fonts);

        song = content.Load<SoundEffect>("sound/songs/" + selectedSong);
        songInstance = song.CreateInstance();
        songInstance.IsLooped = false;

        // Create beat buttons for each direction
        upButton = new BeatButtonUp(arrowTexture);
        downButton = new BeatButtonDown(arrowTexture);
        leftButton = new BeatButtonLeft(arrowTexture);
        rightButton = new BeatButtonRight(arrowTexture);
        
        // Load character
        var animationTextures = new Dictionary<string, Texture2D>
        {
            { "Idle", content.Load<Texture2D>(CharacterSpriteFolder + "Idle") },
            { "DanceUp", content.Load<Texture2D>(CharacterSpriteFolder + "DanceUp") },
            { "DanceDown", content.Load<Texture2D>(CharacterSpriteFolder + "DanceDown") },
            { "DanceLeft", content.Load<Texture2D>(CharacterSpriteFolder + "DanceLeft") },
            { "DanceRight", content.Load<Texture2D>(CharacterSpriteFolder + "DanceRight") },
            { "DanceSplit", content.Load<Texture2D>(CharacterSpriteFolder + "DanceSplit") },
            { "DanceLeftUp", content.Load<Texture2D>(CharacterSpriteFolder + "DanceLeftUp") },
            { "DanceLeftDown", content.Load<Texture2D>(CharacterSpriteFolder + "DanceLeftDown") },
            { "DanceRightUp", content.Load<Texture2D>(CharacterSpriteFolder + "DanceRightUp") },
            { "DanceRightDown", content.Load<Texture2D>(CharacterSpriteFolder + "DanceRightDown") },
            { "DanceUpDown", content.Load<Texture2D>(CharacterSpriteFolder + "DanceUpDown") }
        };
        // Play the sound effect instance
        songInstance.Play();

        beatManager = new BeatManager(arrow);
        beatManager.LoadBeatsFromFile("DanceContext/SongTabs/" + selectedSong + ".txt");
        
        dancingCharacter = new DancingCharacter(animationTextures, new Vector2(503, Origin.Y), beatManager.BeatInterval);
    }

    public void Update(GameTime gameTime)
    {
        // TODO: Change end game to time instead
        if (activeBeats.Count == 0 && beatManager.GetQueueCount() == 0)
        {
            EndGame(scoreManager.Score);
            return;
        }

        var keyboardState = Keyboard.GetState();

        var elapsedTime = (float)stopwatch.Elapsed.TotalSeconds;

        beatManager.Update(gameTime, elapsedTime, activeBeats, scoreManager);

        activeBeats = upButton.Update(gameTime, keyboardState, oldState, activeBeats, scoreManager);
        activeBeats = downButton.Update(gameTime, keyboardState, oldState, activeBeats, scoreManager);
        activeBeats = leftButton.Update(gameTime, keyboardState, oldState, activeBeats, scoreManager);
        activeBeats = rightButton.Update(gameTime, keyboardState, oldState, activeBeats, scoreManager);
        
        scoreManager.Update();
        UpdateBackgroundTint();
        
        dancingCharacter.Update(gameTime, keyboardState);
        
        oldState = keyboardState;
    }
    
    private void UpdateBackgroundTint()
    {
        var multiplier = scoreManager.Multiplier;
        backgroundTint = multiplier switch
        {
            2 => Color.Aqua,
            4 => Color.Yellow,
            1 when backgroundTint != Color.White => Color.White,
            _ => backgroundTint
        };
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, new Rectangle(450, 142, 314, 196), backgroundTint);

        BeatManager.Draw(spriteBatch, activeBeats);
        
        // Draw all the beat buttons
        upButton.Draw(spriteBatch);
        downButton.Draw(spriteBatch);
        leftButton.Draw(spriteBatch);
        rightButton.Draw(spriteBatch);

        scoreManager.Draw(spriteBatch);
        
        dancingCharacter.Draw(spriteBatch);
    }
    
    private void EndGame(int finalScore)
    {
        songInstance.Stop();
        var newScore = new Score
        {
            PlayerName = "",
            Points = finalScore,
            Date = DateTime.Now
        };
        
        var scoreContext = new ScoreScreenContext(soundEffects, fonts, selectedSong)
        {
            NewScore = newScore
        };
        contextManager.SetContext(scoreContext);
    }

}