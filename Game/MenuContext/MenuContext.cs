using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RhythmGame.MenuContext;

public class MenuContext(ContentManager content) : IGameContext
{
    public event Action<int> OnMenuItemSelected;
    private Menu mainMenu;
    private Menu songMenu;
    private Menu currentMenu;

    private KeyboardState oldKeyboardState;
    
    public void LoadContent()
    {
        var font = content.Load<SpriteFont>("font"); // Add a SpriteFont to your Content folder
        
        mainMenu = new Menu(content, font, new Vector2(100, 100));

        mainMenu.AddMenuItem("Start Game");
        mainMenu.AddMenuItem("Options");
        mainMenu.AddMenuItem("Exit", Color.Red);
        
        songMenu = new Menu(content, font, new Vector2(100, 100));
        
        songMenu.AddMenuItem("GET PUMPING!!!");
        songMenu.AddMenuItem("Back", Color.Red);
        
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
        return currentMenu == songMenu ? "Song Menu" : "Main Menu";
    }

    public void SwitchCurrentMenu(string menuName)
    {
        currentMenu = menuName switch
        {
            "Song Menu" => songMenu,
            "Main Menu" => mainMenu,
            _ => currentMenu
        };
    }
}