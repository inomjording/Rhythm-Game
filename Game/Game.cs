using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RhythmGame;

public class BeatGame : Game
{
    private GameContextType gameContextType = GameContextType.MenuContext;
    private IGameContext gameContext;
        
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
        if (gameContextType == GameContextType.MenuContext) gameContext = new MenuContext.MenuContext(Content);
        else gameContext = new DanceContext.DanceContext(Content, "GET PUMPING!!!");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);

        gameContext.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        gameContext.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        graphics.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

        gameContext.Draw(gameTime, spriteBatch);
        
        spriteBatch.End();

        base.Draw(gameTime);
    }
}
