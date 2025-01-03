namespace RhythmGame.MenuContext;

public class MenuItem(string text)
{
    public string Text { get; } = text;
    public bool IsSelected { get; set; }
}
