using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Beats;

public class BeatRight : Beat
{
    public BeatRight(Texture2D texture, float speed = 200, float scale = 1, Color color = default) : base(texture, speed, scale, color)
    {
        position = new Vector2(2 * BeatGame.origin.X, BeatGame.origin.Y);
        direction = Vector2.Normalize(BeatGame.origin - position);
        rotation = MathHelper.PiOver2;
    }
}