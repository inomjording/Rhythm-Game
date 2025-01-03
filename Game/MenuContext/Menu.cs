using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RhythmGame.MenuContext;

public class Menu(SpriteFont font, Vector2 position, Color normalColor, Color selectedColor)
{
    private readonly List<MenuItem> menuItems = [];
    private int selectedIndex;

    private double timeSinceLastInput;
    private const double InputCooldown = 0.2; // Prevent spamming input

    public void AddMenuItem(string text)
    {
        menuItems.Add(new MenuItem(text));
    }

    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
        timeSinceLastInput += gameTime.ElapsedGameTime.TotalSeconds;

        if (timeSinceLastInput > InputCooldown)
        {
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                selectedIndex = (selectedIndex - 1 + menuItems.Count) % menuItems.Count;
                timeSinceLastInput = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menuItems.Count;
                timeSinceLastInput = 0;
            }
        }

        // Update selection
        for (var i = 0; i < menuItems.Count; i++)
        {
            menuItems[i].IsSelected = (i == selectedIndex);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var currentPosition = position;

        foreach (var menuItem in menuItems)
        {
            var color = menuItem.IsSelected ? selectedColor : normalColor;
            spriteBatch.DrawString(font, menuItem.Text, currentPosition, color);

            currentPosition.Y += font.LineSpacing; // Move down to draw the next item
        }
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
}
