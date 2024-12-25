using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Beats;

public class BeatUp : Beat
{
    public BeatUp(Texture2D texture,
        float speed = 200,
        float scale = 1,
        Color color = default) : base(texture,
        speed,
        scale,
        color)
    {
        Position = new Vector2(BeatGame.Origin.X, BeatGame.Origin.Y - 200);
        Direction = Vector2.Normalize(BeatGame.Origin - Position);
    }
}