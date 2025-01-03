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
    
    public void LoadContent()
    {
        var font = content.Load<SpriteFont>("font"); // Add a SpriteFont to your Content folder

        mainMenu = new Menu(font, new Vector2(100, 100), Color.White, Color.Yellow);

        mainMenu.AddMenuItem("Start Game");
        mainMenu.AddMenuItem("Options");
        mainMenu.AddMenuItem("Exit");
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        mainMenu.Update(gameTime, keyboardState);

        if (!keyboardState.IsKeyDown(Keys.Enter)) return;
        var selectedIndex = mainMenu.GetSelectedIndex();
        OnMenuItemSelected?.Invoke(selectedIndex);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        mainMenu.Draw(spriteBatch);
    }
}