using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Text;

namespace RhythmGame.DanceContext;

public class ScoreManager
{
    private int score;
    private readonly HitText scoreText;
    private HitText currentHitText;
    private static readonly Vector2 HitTextPosition = new(280, 150);
    private List<float> hitQueue;
    private readonly SpriteFont font;
    private int chain;
    internal int Multiplier = 1;
    

    public ScoreManager(SpriteFont font)
    {
        hitQueue = [];
        currentHitText = new HitText(HitTextPosition, "");
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
                return Multiplier * 5;
            case < 0.5f:
                IncreaseChain();
                return Multiplier * 3;
            case < 0.95f:
                IncreaseChain();
                return Multiplier * 1;
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

    private static HitText GetTextFromHit(float hit)
    {
        return hit switch
        {
            < 0.25f => new PerfectHitText(HitTextPosition),
            < 0.5f => new GoodHitText(HitTextPosition),
            < 0.95f => new OkHitText(HitTextPosition),
            _ => new MissHitText(HitTextPosition)
        };
    }

    public void Update()
    {
        foreach (var hit in hitQueue.Where(hit => hit != 0f))
        {
            score += CalculateScoreFromHit(hit);
            currentHitText = GetTextFromHit(hit);
            scoreText.Text = score.ToString();
        }

        hitQueue = [];
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, currentHitText.Text, currentHitText.Position, currentHitText.Color);
        spriteBatch.DrawString(font, scoreText.Text, scoreText.Position, scoreText.Color);
    }
}