using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.MenuContext.Menus;

public class MainMenu : Menu
{
    public MainMenu(ContentManager content, SpriteFont font, SpriteFont smallerFont, Vector2 position, Action exit, MenuContext menuContext) : base(content, font, smallerFont, position)
    {
        AddMenuItem("Start Game", () => menuContext.SwitchCurrentMenu("Song Menu"));
        AddMenuItem("Options", () => {});
        AddMenuItem("Exit", exit, Color.Red);
    }
}