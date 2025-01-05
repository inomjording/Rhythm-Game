using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.MenuContext.Menus;

public class MenuFactory(ContentManager content, SpriteFont font, Vector2 position, ContextManager contextManager, MenuContext menuContext, Action exit)
{
    public Menu CreateSongInfoMenu(string selectedSong)
    {
        return new SongInfoMenu(content, font, position, contextManager, menuContext, selectedSong);
    }

    public Menu CreateSongMenu()
    {
        return new SongMenu(content, font, position, this, menuContext);
    }

    public Menu CreateMainMenu()
    {
        return new MainMenu(content, font, position, exit, menuContext);
    }

    public void SwitchCurrentMenu(string menuName)
    {
        menuContext.SwitchCurrentMenu(menuName);
    }
}