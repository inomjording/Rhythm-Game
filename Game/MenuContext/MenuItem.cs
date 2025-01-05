using System;
using Microsoft.Xna.Framework;

namespace RhythmGame.MenuContext;

public class MenuItem(string text, Action action)
{
    public string Text { get; } = text;
    public Color SelectedColor = Color.Yellow;
    public bool IsSelected { get; set; }

    public MenuItem(string text, Action action, Color selectedColor) : this(text, action)
    {
        Text = text;
        SelectedColor = selectedColor;
    }
    
    public void InvokeAction() => action?.Invoke();
}
