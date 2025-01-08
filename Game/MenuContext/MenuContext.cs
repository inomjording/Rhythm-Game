using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.Collections;
using RhythmGame.MenuContext.Menus;

namespace RhythmGame.MenuContext;

public class MenuContext(
    ContentManager content,
    GraphicsDevice graphicsDevice,
    SoundEffectCollection soundEffects,
    FontCollection fonts,
    ContextManager contextManager,
    Action exit) : IGameContext
{
    public event Action<int> OnMenuItemSelected;
    private Menu mainMenu;
    private Menu songMenu;
    public Menu SongInfoMenu;
    private Menu currentMenu;
    private MenuFactory factory;

    private KeyboardState oldKeyboardState;

    public bool ReturnToMainMenu { get; set; } = false;

    public void LoadContent()
    {
        factory = new MenuFactory(content,
            graphicsDevice,
            soundEffects,
            fonts,
            new Vector2(100,
                100),
            contextManager,
            this,
            exit);

        mainMenu = factory.CreateMainMenu();
        songMenu = factory.CreateSongMenu();
        currentMenu = mainMenu;

    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        currentMenu.Update(gameTime, keyboardState);

        if (keyboardState.IsKeyDown(Keys.Enter))
        {
            if (oldKeyboardState.IsKeyDown(Keys.Enter)) return;
            var selectedIndex = currentMenu.GetSelectedIndex();
            OnMenuItemSelected?.Invoke(selectedIndex);
        }

        oldKeyboardState = keyboardState;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        currentMenu.Draw(spriteBatch);
    }

    public string GetNameOfCurrentMenu()
    {
        if (currentMenu == mainMenu)
        {
            return "Main Menu";
        }
        if (currentMenu == songMenu)
        {
            return "Song Menu";
        }
        return currentMenu == SongInfoMenu ? "Song Info Menu" : "";
    }

    public void SwitchCurrentMenu(string menuName)
    {
        currentMenu = menuName switch
        {
            "Song Menu" => songMenu,
            "Main Menu" => mainMenu,
            "Song Info Menu" => SongInfoMenu,
            _ => currentMenu
        };
    }

    public void InvokeSelectedMenuItem(int selectedIndex)
    {
        currentMenu.GetMenuItem(selectedIndex).InvokeAction();
    }
}