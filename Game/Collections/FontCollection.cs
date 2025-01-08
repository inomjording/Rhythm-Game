using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Collections;

public class FontCollection(SpriteFont gameFont, SpriteFont smallerFont)
{
    public readonly SpriteFont GameFont = gameFont;
    public readonly SpriteFont SmallerFont = smallerFont;
}