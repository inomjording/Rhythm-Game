﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using RhythmGame.Beats;

namespace RhythmGame.BeatButtons;

internal class BeatButtonRight : BeatButton
{
    public BeatButtonRight(Texture2D texture) : base(texture)
    {
        position = BeatGame.origin + new Vector2(50, 0);
        rotation = MathHelper.PiOver2;
        associatedKey = Keys.Right;
    }

    public override void CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState)
    {
        foreach (var beat in activeBeats.Where(beat => beat is BeatUp && targetArea.Intersects(beat.GetBoundingBox())))
        {
            activeBeats.Remove(beat);
            break;
        }
    }
}