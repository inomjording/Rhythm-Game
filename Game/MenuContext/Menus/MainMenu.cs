using System;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.MenuContext.Menus;

public class MainMenu : Menu
{
    public MainMenu(ContentManager content, SpriteFont font, Vector2 position, Action exit, MenuContext menuContext) : base(content, font, position)
    {
        AddMenuItem("Start Game", () => menuContext.SwitchCurrentMenu("Song Menu"));
        AddMenuItem("Options", () => {});
        AddMenuItem("Exit", exit, Color.Red);
    }
}