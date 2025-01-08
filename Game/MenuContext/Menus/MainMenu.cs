using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Collections;

namespace RhythmGame.MenuContext.Menus;

public class MainMenu : Menu
{
    public MainMenu(GraphicsDevice graphicsDevice,
        SoundEffectCollection soundEffects,
        FontCollection fonts,
        Vector2 position,
        Action exit,
        ContextManager contextManager,
        MenuContext menuContext) : base(soundEffects,
        fonts,
        position)
    {
        AddMenuItem("Start Game", () => menuContext.SwitchCurrentMenu("Song Menu"));
        AddMenuItem("Options", () =>
        {
            var optionsContext = new OptionsContext.OptionsContext(graphicsDevice, fonts, soundEffects);
            contextManager.SetContext(optionsContext);
        });
        AddMenuItem("Exit", exit, Color.Red);
    }
}