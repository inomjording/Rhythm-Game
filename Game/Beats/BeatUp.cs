using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RythmGame.Beats;

public class BeatUp : Beat
{
    public BeatUp(Texture2D texture, float speed = 200, float scale = 1, Color color = default) : base(texture, speed, scale, color)
    {
        position = new Vector2(BeatGame.origin.X, 0);
        direction = Vector2.Normalize(BeatGame.origin - position);
    }
}