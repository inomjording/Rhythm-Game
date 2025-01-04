using Microsoft.Xna.Framework;

namespace RhythmGame.MenuContext;

public class MenuItem(string text)
{
    public string Text { get; } = text;
    public Color SelectedColor = Color.Yellow;
    public bool IsSelected { get; set; }

    public MenuItem(string text, Color selectedColor) : this(text)
    {
        Text = text;
        SelectedColor = selectedColor;
    }
}
