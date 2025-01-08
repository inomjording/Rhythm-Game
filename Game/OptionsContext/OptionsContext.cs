using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.Collections;

namespace RhythmGame.OptionsContext;

public class OptionsContext(GraphicsDevice graphicsDevice, FontCollection fonts, SoundEffectCollection soundEffects) : IGameContext
{
    private UserSettings userSettings;
    
    public bool ReturnToMainMenu { get; set; }
    private int selectedIndex;
    
    private double timeSinceLastInput;
    private const double InputCooldown = 0.2; // Prevent spamming input
    
    private int masterVolume = 50;
    
    public void LoadContent()
    {
        ReturnToMainMenu = false;
        userSettings = SettingsManager.LoadSettings();
        masterVolume = userSettings.MasterVolume;
    }

    public void Update(GameTime gameTime)
    {
        timeSinceLastInput += gameTime.ElapsedGameTime.TotalSeconds;
        var keyboardState = Keyboard.GetState();

        if (!(timeSinceLastInput > InputCooldown)) return;
        if (keyboardState.IsKeyDown(Keys.Up))
        {
            selectedIndex = (selectedIndex - 1 + 2) % 2;
            soundEffects.PlaySelectSound();
            timeSinceLastInput = 0;
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            selectedIndex = (selectedIndex + 1) % 2;
            soundEffects.PlaySelectSound();
            timeSinceLastInput = 0;
        }

        switch (selectedIndex)
        {
            case 0 when keyboardState.IsKeyDown(Keys.Left):
                timeSinceLastInput = 0;
                masterVolume = masterVolume - 10 < 0 ? 0 : masterVolume -= 10;
                soundEffects.PlaySelectSound();
                SoundEffect.MasterVolume = masterVolume/100f;
                break;
            case 0:
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    timeSinceLastInput = 0;
                    masterVolume = masterVolume + 10 > 100 ? 100 : masterVolume += 10;
                    SoundEffect.MasterVolume = masterVolume/100f;
                    soundEffects.PlaySelectSound();
                }

                break;
            }
            case 1:
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    ReturnToMainMenu = true;
                    soundEffects.PlayConfirmSound();
                    SettingsManager.SaveSettings(userSettings);
                }
                break;
            }
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(fonts.GameFont, "Options", new Vector2(100, 100), Color.White);
        spriteBatch.DrawString(fonts.SmallerFont, $"Volume: {masterVolume}", new Vector2(100, 150), GetTextColor(0));
        spriteBatch.DrawString(fonts.GameFont, "Back", new Vector2(100, 200), GetTextColor(1));
        
        var bar = new Texture2D(graphicsDevice, 1, 1);
        bar.SetData([Color.White]);
        spriteBatch.Draw(bar, new Rectangle(250, 150, masterVolume * 3, 20), Color.Green);

        spriteBatch.DrawString(fonts.SmallerFont, "The settings aren't saved for some reason...\nThe volume is updated though!", new Vector2(100, 300), Color.Gray);
    }

    private Color GetTextColor(int index)
    {
        return index == selectedIndex ? index == 1 ? Color.Red : Color.Yellow : Color.White;
    }
}