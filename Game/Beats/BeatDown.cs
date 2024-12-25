using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Beats;

public class BeatDown : Beat
{
    public BeatDown(Texture2D texture, float speed = 200, float scale = 1, Color color = default) : base(texture, speed, scale, color)
    {
        position = new Vector2(BeatGame.origin.X, BeatGame.origin.Y + 200);
        direction = Vector2.Normalize(BeatGame.origin - position);
        rotation = MathHelper.Pi;
    }
}