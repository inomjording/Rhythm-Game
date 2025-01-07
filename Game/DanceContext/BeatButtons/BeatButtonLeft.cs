using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.DanceContext.Beats;

namespace RhythmGame.DanceContext.BeatButtons;

internal class BeatButtonLeft : BeatButton
{
    public BeatButtonLeft(Texture2D texture) : base(texture)
    {
        Position = DanceContext.Origin - new Vector2(50, 0);
        Rotation = -MathHelper.PiOver2;
        AssociatedKey = Keys.Left;
        Center = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        TargetArea = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }

    protected override List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState, ScoreManager scoreManager)
    {
        var beats = new List<Beat>();
        foreach (var beat in activeBeats)
        {
            if (beat is BeatLeft)
            {
                var collision = CollisionMeasureX(beat);
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
