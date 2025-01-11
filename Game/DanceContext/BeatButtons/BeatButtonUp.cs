using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.DanceContext.Beats;

namespace RhythmGame.DanceContext.BeatButtons;

internal class BeatButtonUp : BeatButton
{
    public BeatButtonUp(Texture2D texture) : base(texture)
    {
        Position = DanceContext.Origin - new Vector2(0, DistanceFromOrigin);
        Rotation = 0f;
        AssociatedKey = Keys.Up;
        Center = new Vector2(texture.Width / 2f, texture.Height / 2f);
        TargetArea = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }

    protected override List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState, ScoreManager scoreManager)
    {
        var beats = new List<Beat>();
        foreach (var beat in activeBeats)
        {
            if (beat is BeatUp)
            {
                var collision = CollisionMeasureY(beat);
                if (collision >= 1)
                {
                    beats.Add(beat);
                }
                else
                {
                    scoreManager.QueueHit(collision);
                }
            }
            else
            {
                beats.Add(beat);
            }
        }

        return beats;
    }
}
