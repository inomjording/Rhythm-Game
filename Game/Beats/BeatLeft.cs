using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Beats;

public class BeatLeft : Beat
{
    public BeatLeft(Texture2D texture, float speed = 200, float scale = 1, Color color = default) : base(texture, speed, scale, color)
    {
        position = new Vector2(BeatGame.origin.X - 200, BeatGame.origin.Y);
        direction = Vector2.Normalize(BeatGame.origin - position);
        rotation = -MathHelper.PiOver2;
    }
}