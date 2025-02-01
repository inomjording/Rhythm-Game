using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Collections;

namespace RhythmGame.MenuContext.Menus;

public class MenuFactory(ContentManager content, GraphicsDevice graphicsDevice, SoundEffectCollection soundEffects, FontCollection fonts, Vector2 position, ContextManager contextManager, MenuContext menuContext, Action exit)
{
    public Menu CreateSongInfoMenu(string selectedSong)
    {
        return new SongInfoMenu(content, soundEffects, fonts, position, contextManager, menuContext, selectedSong);
    }

    public Menu CreateSongMenu()
    {
        return new SongMenu(soundEffects, fonts, position, this, menuContext);
    }

    public Menu CreateMainMenu()
    {
        return new MainMenu(content, graphicsDevice, soundEffects, fonts, position, exit, contextManager, menuContext);
    }

    public void SwitchCurrentMenu(string menuName)
    {
        menuContext.SwitchCurrentMenu(menuName);
    }
}