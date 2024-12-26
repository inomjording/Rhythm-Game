using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using RhythmGame.Beats;

namespace RhythmGame.BeatButtons;

internal class BeatButtonDown : BeatButton
{
    public BeatButtonDown(Texture2D texture) : base(texture)
    {
        Position = BeatGame.Origin + new Vector2(0, 50);
        Rotation = MathHelper.Pi;
        AssociatedKey = Keys.Down;
        Center = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        TargetArea = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }

    protected override List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState, ScoreManager scoreManager)
    {
        var beats = new List<Beat>();
        foreach (var beat in activeBeats)
        {
            if (beat is BeatDown)
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
