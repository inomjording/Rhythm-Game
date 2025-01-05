using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.ScoreContext;

public class ScoreScreenContext(SpriteFont font) : IGameContext
{
    private readonly List<Score> scores = ScoreScreenManager.LoadScores();

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, "Score Screen", new Vector2(100, 50), Color.White);

        // Display top scores
        var yPosition = 100;
        foreach (var scoreText in scores.Select(score => $"{score.PlayerName}.....................{score.Points}"))
        {
            spriteBatch.DrawString(font, scoreText, new Vector2(100, yPosition), Color.White);
            yPosition += 30;
        }

        spriteBatch.DrawString(font, "Press ESC to EXIT\n(going back is not implemented. You are stuck here.)", new Vector2(100, yPosition + 20), Color.Gray);
    }

    public void LoadContent()
    {
        
    }

    public void Update(GameTime gameTime)
    {
    }
}
