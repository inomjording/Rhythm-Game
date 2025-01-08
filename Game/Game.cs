using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.Collections;
using RhythmGame.OptionsContext;

namespace RhythmGame;

public class BeatGame : Game
{
    private ContextManager contextManager;
    private MenuContext.MenuContext menuContext;

    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    private SoundEffectCollection soundEffects;

    private FontCollection fonts;
    
    private UserSettings userSettings;

    public BeatGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        userSettings = SettingsManager.LoadSettings();
        SoundEffect.MasterVolume = 0.5f;
        
        var confirmSound = Content.Load<SoundEffect>("sound/sound-effects/Confirm effect");
        var selectSound = Content.Load<SoundEffect>("sound/sound-effects/Select effect");
        soundEffects = new SoundEffectCollection(confirmSound, selectSound);
        
        var gameFont = Content.Load<SpriteFont>("font");
        var smallerFont = Content.Load<SpriteFont>("smaller font");
        fonts = new FontCollection(gameFont, smallerFont);
        
        contextManager = new ContextManager();

        menuContext = new MenuContext.MenuContext(Content, GraphicsDevice, soundEffects, fonts, contextManager, Exit);
        menuContext.OnMenuItemSelected += HandleMenuSelection;

        contextManager.SetContext(menuContext);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        
        contextManager.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (contextManager.IsReturnToMenuContext()) contextManager.SetContext(menuContext);
        
        contextManager.Update(gameTime);

        base.Update(gameTime);
    }
    
    private void HandleMenuSelection(int selectedIndex)
    {
        soundEffects.PlayConfirmSound();
        menuContext.InvokeSelectedMenuItem(selectedIndex);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        graphics.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        contextManager.Draw(gameTime, spriteBatch);
        
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
