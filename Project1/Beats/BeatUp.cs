using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace RythmGame.Beats
{
    public class BeatUp : Beat
    {
        public BeatUp(Texture2D texture, float speed = 200, float scale = 1) : base(texture, speed, scale)
        {
            position = new Vector2(Game1.origin.X, 0);
            direction = Vector2.Normalize(Game1.origin - position);
        }
    }
}
