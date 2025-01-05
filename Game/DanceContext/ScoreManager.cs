using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.ScoreContext;
using RhythmGame.Text;

namespace RhythmGame;

public class ScoreManager
{
    private int score;
    private readonly HitText scoreText;
    private readonly HitText currentHitText;
    private List<float> hitQueue;
    private readonly SpriteFont font;
    private int chain;
    internal int Multiplier = 1;
    

    public ScoreManager(SpriteFont font)
    {
        hitQueue = [];
        var hitTextPosition = new Vector2(300, 150);
        currentHitText = new HitText(hitTextPosition, "");
        this.font = font;
        
        var scoreTextPosition = new Vector2(420, 120);
        scoreText = new HitText(scoreTextPosition, score.ToString());
    }

    public void QueueHit(float hit)
    {
        hitQueue.Add(hit);
    }

    private int CalculateScoreFromHit(float hit)
    {
        switch (hit)
        {
            case < 0.25f:
                IncreaseChain();
                return Multiplier * 500;
            case < 0.5f:
                IncreaseChain();
                return Multiplier * 300;
            case < 0.95f:
                IncreaseChain();
                return Multiplier * 100;
            default:
                chain = 0;
                Multiplier = 1;
                return 0;
        }
    }

    private void IncreaseChain()
    {
        chain++;
        Multiplier = chain switch
        {
            >= 30 => 4,
            >= 15 => 2,
            _ => Multiplier
        };
    }

    private static string GetTextFromHit(float hit)
    {
        return hit switch
        {
            < 0.25f => "Perfect!",
            < 0.5f => "Good!",
            < 0.95f => "Ok!",
            _ => "Miss"
        };
    }

    public void Update()
    {
        foreach (var hit in hitQueue.Where(hit => hit != 0f))
        {
            score += CalculateScoreFromHit(hit);
            currentHitText.Text = GetTextFromHit(hit);
            scoreText.Text = score.ToString();
        }

        hitQueue = [];
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, currentHitText.Text, currentHitText.position, Color.White);
        spriteBatch.DrawString(font, scoreText.Text, scoreText.position, Color.White);
    }
}