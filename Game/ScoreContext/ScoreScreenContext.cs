using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.ScoreContext;

public class ScoreScreenContext(SpriteFont font, string selectedSong) : IGameContext
{
    private readonly List<Score> scores = ScoreScreenManager.LoadScores()[selectedSong];

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, selectedSong, new Vector2(100, 50), Color.White);

        // Display top scores
        var yPosition = 100;
        foreach (var scoreText in scores.Select(score => FormatScoreString(score.PlayerName, score.Points)))
        {
            spriteBatch.DrawString(font, scoreText, new Vector2(100, yPosition), Color.White);
            yPosition += 30;
        }

        spriteBatch.DrawString(font, "Press ESC to EXIT.\nGoing back is not implemented.\nYou are stuck here.", new Vector2(100, yPosition + 20), Color.Gray);
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
    }
}
