using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame;

public class ContextManager
{
    private IGameContext currentContext;

    public void SetContext(IGameContext newContext)
    {
        currentContext = newContext;
    }

    public void LoadContent()
    {
        currentContext.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        currentContext?.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        currentContext?.Draw(gameTime, spriteBatch);
    }
}