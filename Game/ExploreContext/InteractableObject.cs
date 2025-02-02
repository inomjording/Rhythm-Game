using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Collections;

namespace RhythmGame.ExploreContext;

public class InteractableObject(string name, int leftBorder, int rightBorder, string[] text)
{
    private readonly Vector2 nameTextPosition = new(30, 40);
    
    private readonly Vector2 visibleTextPosition = new(30, 430);
    private int visibleTextPage = -1;
    private string visibleText = "";

    public bool IsWithinBorder(int characterPosition)
    {
        return characterPosition >= leftBorder && characterPosition <= rightBorder;
    }

    public void Reset()
    {
        visibleTextPage = -1;
        visibleText = "";
    }

    public void Update()
    {
        if (visibleTextPage != -1 && visibleText != text[visibleTextPage])
        {
            visibleText += text[visibleTextPage][visibleText.Length];
        }
    }

    public void UpdateText()
    {
        if (visibleTextPage != -1 && visibleText != text[visibleTextPage])
        {
            visibleText = text[visibleTextPage];
        }
        else if (visibleTextPage == text.Length - 1)
        {
            visibleText = "";
            visibleTextPage = -1;
        }
        else
        {
            visibleText = "";
            visibleTextPage++;
        }
    }

    public void Draw(SpriteBatch spriteBatch, FontCollection fonts)
    {
        spriteBatch.DrawString(fonts.GameFont, name, nameTextPosition, Color.Gold);
        spriteBatch.DrawString(fonts.SmallerFont, visibleText, visibleTextPosition, Color.White);
    }
}