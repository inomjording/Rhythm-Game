using Microsoft.Xna.Framework;

namespace RhythmGame.DanceContext.Text;

public class HitText(Vector2 position, string text)
{
    public Vector2 Position = position;
    public string Text = text;
    public Color Color = Color.White;

    protected HitText(Vector2 position, string text, Color color) : this(position, text)
    {
        Color = color;
    }
}

public class PerfectHitText(Vector2 position) : HitText(position, "Perfect!", Color.Gold);

public class GoodHitText(Vector2 position) : HitText(position, "Good!", Color.Magenta);

public class OkHitText(Vector2 position) : HitText(position, "Ok!", Color.Lime);

public class MissHitText(Vector2 position) : HitText(position, "Miss", Color.Gray);
