using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Text;

public class HitText
{
    public Vector2 position;
    public string Text;

    public HitText(Vector2 position, string text)
    {
        this.position = position;
        Text = text;
    }
    
    
}