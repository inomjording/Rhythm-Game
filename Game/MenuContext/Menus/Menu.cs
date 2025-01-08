using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.Collections;

namespace RhythmGame.MenuContext.Menus;

public class Menu(SoundEffectCollection soundEffects, FontCollection fonts, Vector2 position)
{
    private readonly List<MenuItem> menuItems = [];
    private int selectedIndex;

    private double timeSinceLastInput;
    private const double InputCooldown = 0.2; // Prevent spamming input

    private readonly Color normalColor = Color.White;

    protected void AddMenuItem(string text, Action action)
    {
        menuItems.Add(new MenuItem(text, action));
    }

    protected void AddMenuItem(string text, Action action, Color selectedColor)
    {
        menuItems.Add(new MenuItem(text, action, selectedColor));
    }

    protected void AddMenuItem(string text, Action action, string description)
    {
        menuItems.Add(new MenuItem(text, action, description));
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
                soundEffects.PlaySelectSound();
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                selectedIndex = (selectedIndex + 1) % menuItems.Count;
                timeSinceLastInput = 0;
                soundEffects.PlaySelectSound();
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
            var color = menuItem.IsSelected ? menuItem.SelectedColor : normalColor;
            spriteBatch.DrawString(fonts.GameFont, menuItem.Text, currentPosition, color);

            currentPosition.Y += fonts.GameFont.LineSpacing; // Move down to draw the next item
        }
        
        var description = menuItems[selectedIndex].Description ?? "";
        spriteBatch.DrawString(fonts.SmallerFont, description, new Vector2(400, 200), Color.Gray);
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
    
    public MenuItem GetMenuItem(int index) => menuItems[index];
}
