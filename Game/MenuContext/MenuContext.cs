using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RhythmGame.MenuContext;

public class MenuContext(ContentManager content, SpriteFont gameFont) : IGameContext
{
    public event Action<int> OnMenuItemSelected;
    private Menu mainMenu;
    private Menu songMenu;
    private Menu songInfoMenu;
    private Menu currentMenu;

    private KeyboardState oldKeyboardState;
    
    public void LoadContent()
    {
        mainMenu = new Menu(content, gameFont, new Vector2(100, 100));

        mainMenu.AddMenuItem("Start Game");
        mainMenu.AddMenuItem("Options");
        mainMenu.AddMenuItem("Exit", Color.Red);
        
        songMenu = new Menu(content, gameFont, new Vector2(100, 100));
        
        songMenu.AddMenuItem("GET PUMPING!!!");
        songMenu.AddMenuItem("Holy melancholy");
        songMenu.AddMenuItem("7");
        songMenu.AddMenuItem("Back", Color.Red);
        
        songInfoMenu = new Menu(content, gameFont, new Vector2(100, 100));
        songInfoMenu.AddMenuItem("Start");
        songInfoMenu.AddMenuItem("Score");
        songInfoMenu.AddMenuItem("Back", Color.Red);
        
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
        return currentMenu == songInfoMenu ? "Song Info Menu" : "";
    }

    public void SwitchCurrentMenu(string menuName)
    {
        currentMenu = menuName switch
        {
            "Song Menu" => songMenu,
            "Main Menu" => mainMenu,
            "Song Info Menu" => songInfoMenu,
            _ => currentMenu
        };
    }
}