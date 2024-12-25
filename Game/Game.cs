using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using RhythmGame.BeatButtons;
using RhythmGame.Beats;
using RhythmGame.CharacterSprite;

namespace RhythmGame;

public class BeatGame : Microsoft.Xna.Framework.Game
{
    Texture2D arrowTexture;
    Texture2D arrow;
    Vector2 arrowPosition;
    private Texture2D background;
        
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static Vector2 origin = new Vector2(200, 240);

    // Create beat buttons for each direction
    BeatButtonUp upButton;
    BeatButtonDown downButton;
    BeatButtonLeft leftButton;
    BeatButtonRight rightButton;

        
    BeatManager beatManager;
    List<Beat> activeBeats = new();

    // Audio objects
    SoundEffect soundEffect;
    private SoundEffectInstance soundEffectInstance;

    // Character
    private DancingCharacter dancingCharacter;

    public BeatGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        arrowPosition = new Vector2(75, 0);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        arrowTexture = Content.Load<Texture2D>("active-arrow");
        arrow = Content.Load<Texture2D>("full-arrow");
        background = Content.Load<Texture2D>("bg");

        soundEffect = Content.Load<SoundEffect>("GET PUMPING!!!");
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
            { "Idle", Content.Load<Texture2D>("Idle") },
            { "DanceUp", Content.Load<Texture2D>("DanceUp") },
            { "DanceDown", Content.Load<Texture2D>("DanceDown") },
            { "DanceLeft", Content.Load<Texture2D>("DanceLeft") },
            { "DanceRight", Content.Load<Texture2D>("DanceRight") },
            { "DanceSplit", Content.Load<Texture2D>("DanceSplit") },
            { "DanceLeftUp", Content.Load<Texture2D>("DanceLeftUp") },
            { "DanceLeftDown", Content.Load<Texture2D>("DanceLeftDown") },
            { "DanceRightUp", Content.Load<Texture2D>("DanceRightUp") },
            { "DanceRightDown", Content.Load<Texture2D>("DanceRightDown") },
            { "DanceUpDown", Content.Load<Texture2D>("DanceUpDown") }
        };
        dancingCharacter = new DancingCharacter(animationTextures, new Vector2(500, origin.Y));

        // Play the sound effect instance
        soundEffectInstance.Play();

        beatManager = new BeatManager(0.5f*0.38709677419f, origin, arrow, 300f);
        beatManager.LoadBeatsFromFile("../../../GET PUMPING!!!.txt");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var keyboardState = Keyboard.GetState();

        arrowPosition.Y += 2;

        beatManager.Update(gameTime, activeBeats);

        activeBeats = upButton.Update(gameTime, keyboardState, activeBeats);
        activeBeats = downButton.Update(gameTime, keyboardState, activeBeats);
        activeBeats = leftButton.Update(gameTime, keyboardState, activeBeats);
        activeBeats = rightButton.Update(gameTime, keyboardState, activeBeats);
        
        dancingCharacter.Update(gameTime, keyboardState);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _graphics.GraphicsDevice.Clear(Color.Black);


        _spriteBatch.Begin();
        _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);

        beatManager.Draw(_spriteBatch, activeBeats);
        
        // Draw all the beat buttons
        upButton.Draw(_spriteBatch);
        downButton.Draw(_spriteBatch);
        leftButton.Draw(_spriteBatch);
        rightButton.Draw(_spriteBatch);
        
        dancingCharacter.Draw(_spriteBatch);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}