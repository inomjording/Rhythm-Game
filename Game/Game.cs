using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RhythmGame;

public class BeatGame : Game
{
    private ContextManager contextManager;
    private MenuContext.MenuContext menuContext;
    private DanceContext.DanceContext danceContext;
        
    private readonly GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

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
        if (menuContext.GetNameOfCurrentMenu() == "Main Menu")
        {
            switch (selectedIndex)
            {
                case 0:
                    menuContext.SwitchCurrentMenu("Song Menu");
                    break;
                
                case 1:
                    // Open options (create and manage an OptionsContext if needed)
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
