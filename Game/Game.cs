using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using RhythmGame.BeatButtons;
using RhythmGame.Beats;
using RhythmGame.CharacterSprite;

namespace RhythmGame;

public class BeatGame : Game
{
    private Texture2D arrowTexture;
    private Texture2D arrow;
    private Vector2 arrowPosition;
    private Texture2D background;
    public KeyboardState oldState;
        
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

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

    public BeatGame()
    {
        graphics = new GraphicsDeviceManager(this);
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
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);
        
        arrowTexture = Content.Load<Texture2D>("active-arrow");
        arrow = Content.Load<Texture2D>("full-arrow");
        background = Content.Load<Texture2D>("bg2");

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
        dancingCharacter = new DancingCharacter(animationTextures, new Vector2(463, Origin.Y));

        // Play the sound effect instance
        soundEffectInstance.Play();

        beatManager = new BeatManager(0.5f*0.38709677419f, Origin, arrow, 300f);
        beatManager.LoadBeatsFromFile("../../../GET PUMPING!!!.txt");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var keyboardState = Keyboard.GetState();

        arrowPosition.Y += 2;

        beatManager.Update(gameTime, activeBeats);

        activeBeats = upButton.Update(gameTime, keyboardState, oldState, activeBeats);
        activeBeats = downButton.Update(gameTime, keyboardState, oldState, activeBeats);
        activeBeats = leftButton.Update(gameTime, keyboardState, oldState, activeBeats);
        activeBeats = rightButton.Update(gameTime, keyboardState, oldState, activeBeats);
        
        dancingCharacter.Update(gameTime, keyboardState);
        
        oldState = keyboardState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        graphics.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();
        spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);

        BeatManager.Draw(spriteBatch, activeBeats);
        
        // Draw all the beat buttons
        upButton.Draw(spriteBatch);
        downButton.Draw(spriteBatch);
        leftButton.Draw(spriteBatch);
        rightButton.Draw(spriteBatch);
        
        dancingCharacter.Draw(spriteBatch);
        
        spriteBatch.End();

        base.Draw(gameTime);
    }
}