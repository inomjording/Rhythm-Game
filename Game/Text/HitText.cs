using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Text;

public class HitText
{
    Vector2 position;
    private SpriteFont font;
    private string text;

    public HitText(Vector2 position, string text, SpriteFont font)
    {
        this.position = position;
        this.text = text;
        this.font = font;
    }
    
    
}