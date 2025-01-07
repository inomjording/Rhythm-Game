using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.Utils;

namespace RhythmGame.ScoreContext;

public class ScoreScreenContext(ContentManager content, SpriteFont font, SpriteFont smallerFont, string selectedSong) : IGameContext
{
    private readonly List<Score> scores = ScoreScreenManager.LoadScores()[selectedSong];
    public bool ReturnToMainMenu { get; set; }
    public Score NewScore;
    
    private KeyboardState lastKeyboardState;
    private Keys lastPressedKey;
    private double timeSinceLastInput;
    private const double InputCooldown = 0.2;
    
    private readonly SoundEffectInstance selectSoundInstance = content.Load<SoundEffect>("sound/sound-effects/Select effect").CreateInstance();
    private readonly SoundEffectInstance confirmSoundInstance = content.Load<SoundEffect>("sound/sound-effects/Confirm effect").CreateInstance();

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if (NewScore == null)
        {
            DrawScores(spriteBatch);
        }
        else
        {
            DrawEnterName(spriteBatch);
        }
    }

    private void DrawScores(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, selectedSong, new Vector2(100, 50), Color.White);

        // Display top scores
        var yPosition = 100;
        foreach (var scoreText in scores.Select(score => FormatScoreString(score.PlayerName, score.Points)))
        {
            spriteBatch.DrawString(smallerFont, scoreText, new Vector2(100, yPosition), Color.White);
            yPosition += 30;
        }

        spriteBatch.DrawString(smallerFont, "Press BACKSPACE to return.", new Vector2(100, yPosition + 20), Color.Gray);
    }

    private void DrawEnterName(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "Enter Your Name:", new Vector2(100, 50), Color.White);
        spriteBatch.DrawString(smallerFont, NewScore.PlayerName + "~", new Vector2(100, 80), Color.White);
        spriteBatch.DrawString(font, "Confirm", new Vector2(100, 130), Color.Yellow);
    }

    private static string FormatScoreString(string playerName, int score)
    {
        var numberOfDots = 45 - playerName.Length - score.ToString().Length;
        return playerName + new string('.', numberOfDots) + score;
    }
    
    public void LoadContent()
    {
        
    }

    public void Update(GameTime gameTime)
    {
        var keyBoardState = Keyboard.GetState();
        
        if (keyBoardState.IsKeyDown(lastPressedKey)) return;
            
        if (keyBoardState.GetPressedKeys().Length == 0)
        {
            lastPressedKey = Keys.None;
            return;
        }
        
        if (NewScore == null)
        {
            if (keyBoardState.IsKeyDown(Keys.Back))
            {
                ReturnToMainMenu = true;
            }
            return;
        }
        
        if (keyBoardState.IsKeyDown(Keys.Enter))
        {
            ScoreScreenManager.AddScore(NewScore, selectedSong);
            confirmSoundInstance.Play();
            NewScore = null;
            return;
        }
        
        if (keyBoardState.IsKeyDown(Keys.Back))
        {
            lastPressedKey = Keys.Back;
            if (NewScore.PlayerName != "") NewScore.PlayerName = NewScore.PlayerName[..^1];
            selectSoundInstance.Stop();
            selectSoundInstance.Play();
            return;
        }

        var shiftDown = keyBoardState.IsKeyDown(Keys.LeftShift) || keyBoardState.IsKeyDown(Keys.RightShift);
        var lettersAndNumbers = keyBoardState.GetPressedKeys().Where(key =>
            KeyUtils.IsKeyAChar(key) || KeyUtils.IsKeyADigit(key)
        ).Select(key => key);
        lastPressedKey = lettersAndNumbers.FirstOrDefault();
        if (lastPressedKey != default)
        {
            selectSoundInstance.Stop();
            selectSoundInstance.Play();
        }
        NewScore.PlayerName += KeyUtils.GetCharFromKey(lastPressedKey, shiftDown);
    }
}
