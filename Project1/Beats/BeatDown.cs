using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame.Beats
{
    public class BeatDown : Beat
    {
        public BeatDown(Texture2D texture, float speed = 200, float scale = 1) : base(texture, speed, scale)
        {
            position = new Vector2(Game1.origin.X, 2 * Game1.origin.Y);
            direction = Vector2.Normalize(Game1.origin - position);
            rotation = MathHelper.Pi;
        }
    }
}
