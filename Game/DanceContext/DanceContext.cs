using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.CharacterSprite;
using RhythmGame.DanceContext.BeatButtons;
using RhythmGame.DanceContext.Beats;

namespace RhythmGame.DanceContext;

public class DanceContext(ContentManager content, string selectedSong) : IGameContext
{
    private Texture2D arrowTexture;
    private Texture2D arrow;
    private Vector2 arrowPosition = new(75, 0);
    private Texture2D background;
    private Color backgroundTint = Color.White;
    private KeyboardState oldState;
    private ScoreManager scoreManager;
    SpriteFont font;
    
    public static readonly Vector2 Origin = new(200, 240);

    // Create beat buttons for each direction
    private BeatButtonUp upButton;
    private BeatButtonDown downButton;
    private BeatButtonLeft leftButton;
    private BeatButtonRight rightButton;


    private BeatManager beatManager;
    private List<Beat> activeBeats = [];

    // Audio objects
    private SoundEffect soundEffect;
    private SoundEffectInstance soundEffectInstance;

    // Character
    private DancingCharacter dancingCharacter;

    private Stopwatch stopwatch = Stopwatch.StartNew();

    public void LoadContent()
    {
        arrowTexture = content.Load<Texture2D>("active-arrow");
        arrow = content.Load<Texture2D>("full-arrow");
        background = content.Load<Texture2D>("bg2");
        font = content.Load<SpriteFont>("font");
        scoreManager = new ScoreManager(font);

        soundEffect = content.Load<SoundEffect>(selectedSong);
        soundEffectInstance = soundEffect.CreateInstance();
        soundEffectInstance.IsLooped = true;

        // Create beat buttons for each direction
        upButton = new BeatButtonUp(arrowTexture);
        downButton = new BeatButtonDown(arrowTexture);
        leftButton = new BeatButtonLeft(arrowTexture);
        rightButton = new BeatButtonRight(arrowTexture);
        
        // Load character
        var animationTextures = new Dictionary<string, Texture2D>
        {
            { "Idle", content.Load<Texture2D>("Idle") },
            { "DanceUp", content.Load<Texture2D>("DanceUp") },
            { "DanceDown", content.Load<Texture2D>("DanceDown") },
            { "DanceLeft", content.Load<Texture2D>("DanceLeft") },
            { "DanceRight", content.Load<Texture2D>("DanceRight") },
            { "DanceSplit", content.Load<Texture2D>("DanceSplit") },
            { "DanceLeftUp", content.Load<Texture2D>("DanceLeftUp") },
            { "DanceLeftDown", content.Load<Texture2D>("DanceLeftDown") },
            { "DanceRightUp", content.Load<Texture2D>("DanceRightUp") },
            { "DanceRightDown", content.Load<Texture2D>("DanceRightDown") },
            { "DanceUpDown", content.Load<Texture2D>("DanceUpDown") }
        };
        dancingCharacter = new DancingCharacter(animationTextures, new Vector2(463, Origin.Y));

        // Play the sound effect instance
        soundEffectInstance.Play();

        beatManager = new BeatManager(arrow);
        beatManager.LoadBeatsFromFile("DanceContext/SongTabs/" + selectedSong + ".txt");
    }

    public void Update(GameTime gameTime)
    {
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
        spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), backgroundTint);

        BeatManager.Draw(spriteBatch, activeBeats);
        
        // Draw all the beat buttons
        upButton.Draw(spriteBatch);
        downButton.Draw(spriteBatch);
        leftButton.Draw(spriteBatch);
        rightButton.Draw(spriteBatch);

        scoreManager.Draw(spriteBatch);
        
        dancingCharacter.Draw(spriteBatch);
    }
}