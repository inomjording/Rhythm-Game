using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame.Beats
{
    public class BeatRight : Beat
    {
        public BeatRight(Texture2D texture, float speed = 200, float scale = 1) : base(texture, speed, scale)
        {
            position = new Vector2(2 * Game1.origin.X, Game1.origin.Y);
            direction = Vector2.Normalize(Game1.origin - position);
            rotation = MathHelper.PiOver2;
        }
    }
}
