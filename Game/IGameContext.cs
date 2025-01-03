using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame;

public interface IGameContext
{ 
    void LoadContent();
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}