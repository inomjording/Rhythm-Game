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
        position = BeatGame.origin + new Vector2(0, 50);
        rotation = MathHelper.Pi;
        associatedKey = Keys.Down;
        center = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        targetArea = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
    }

    public override List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState)
    {
        List<Beat> beats = new List<Beat>();
        foreach (Beat beat in activeBeats)
        {
            if (beat is BeatDown && targetArea.Intersects(beat.GetBoundingBox())) continue;
            beats.Add(beat);
        }
        return beats;
    }
}