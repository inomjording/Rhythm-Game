using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame.Beats;

public class BeatLeft : Beat
{
    public BeatLeft(Texture2D texture, float speed = 200, float scale = 1) : base(texture, speed, scale)
    {
        position = new Vector2(0, BeatGame.origin.Y);
        direction = Vector2.Normalize(BeatGame.origin - position);
        rotation = -MathHelper.PiOver2;
    }
}