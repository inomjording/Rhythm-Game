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

    public BeatGame()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        menuContext = new MenuContext.MenuContext(Content);
        menuContext.OnMenuItemSelected += HandleMenuSelection;

        contextManager = new ContextManager();
        contextManager.SetContext(menuContext);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        confirmSound = Content.Load<SoundEffect>("Confirm effect");
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
        if (menuContext.GetNameOfCurrentMenu() == "Main Menu")
        {
            switch (selectedIndex)
            {
                case 0:
                    menuContext.SwitchCurrentMenu("Song Menu");
                    break;
                
                case 1:
                    var font = Content.Load<SpriteFont>("font");
                    var scoreContext = new ScoreScreenContext(font);
                    contextManager.SetContext(scoreContext);
                    break;

                case 2:
                    // Exit the game
                    Exit();
                    break;
            }
        }
        else
        {
            switch (selectedIndex)
            {
                case 0:
                    danceContext = new DanceContext.DanceContext(Content, "GET PUMPING!!!");
                    contextManager.SetContext(danceContext);
                    contextManager.LoadContent();
                    break;
                
                case 1:
                    danceContext = new DanceContext.DanceContext(Content, "Holy melancholy");
                    contextManager.SetContext(danceContext);
                    contextManager.LoadContent();
                    break;
                
                case 2:
                    danceContext = new DanceContext.DanceContext(Content, "7");
                    contextManager.SetContext(danceContext);
                    contextManager.LoadContent();
                    break;

                case 3:
                    menuContext.SwitchCurrentMenu("Main Menu");
                    break;
                
            }
        }
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
