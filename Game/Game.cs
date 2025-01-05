using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.ScoreContext;

namespace RhythmGame;

public class BeatGame : Game
{
    private ContextManager contextManager;
    private MenuContext.MenuContext menuContext;
    private DanceContext.DanceContext danceContext;
        
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    private SoundEffect confirmSound;
    private SoundEffectInstance confirmSoundInstance;

    private SpriteFont gameFont;

    private string selectedSong;

    public BeatGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        gameFont = Content.Load<SpriteFont>("font");
        
        contextManager = new ContextManager();

        menuContext = new MenuContext.MenuContext(Content, gameFont, contextManager, Exit);
        menuContext.OnMenuItemSelected += HandleMenuSelection;

        contextManager.SetContext(menuContext);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        confirmSound = Content.Load<SoundEffect>("sound/sound-effects/Confirm effect");
        confirmSoundInstance = confirmSound.CreateInstance();
        
        contextManager.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        contextManager.Update(gameTime);

        base.Update(gameTime);
    }
    
    private void HandleMenuSelection(int selectedIndex)
    {
        confirmSoundInstance.Stop();
        confirmSoundInstance.Play();
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
