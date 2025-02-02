using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Collections;

namespace RhythmGame.MenuContext.Menus;

public class MainMenu : Menu
{
    public MainMenu(ContentManager content,
        GraphicsDevice graphicsDevice,
        SoundEffectCollection soundEffects,
        FontCollection fonts,
        Vector2 position,
        Action exit,
        ContextManager contextManager,
        MenuContext menuContext) : base(soundEffects,
        fonts,
        position)
    {
        AddMenuItem("Start Game", () =>
        {
            var exploreContext = new ExploreContext.ExploreContext(content, fonts);
            contextManager.SetContext(exploreContext);
            contextManager.LoadContent();
        });
        AddMenuItem("Play Songs", () => menuContext.SwitchCurrentMenu("Song Menu"));
        AddMenuItem("Options", () =>
        {
            var optionsContext = new OptionsContext.OptionsContext(graphicsDevice, fonts, soundEffects);
            contextManager.SetContext(optionsContext);
            contextManager.LoadContent();
        });
        AddMenuItem("Exit", exit, Color.Red);
    }
}