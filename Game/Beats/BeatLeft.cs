using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Beats;

public class BeatLeft : Beat
{
    public BeatLeft(Texture2D texture,
        float speed = 200,
        float scale = 1,
        Color color = default) : base(texture,
        speed,
        scale,
        color)
    {
        Position = new Vector2(BeatGame.Origin.X - 200, BeatGame.Origin.Y);
        Direction = Vector2.Normalize(BeatGame.Origin - Position);
        Rotation = -MathHelper.PiOver2;
    }
}
