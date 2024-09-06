using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame.Beats;
using System.Collections.Generic;

namespace RythmGame.BeatButtons;

internal class BeatButtonDown : BeatButton
{
    public BeatButtonDown(Texture2D texture) : base(texture)
    {
        position = BeatGame.origin + new Vector2(0, 50);
        rotation = MathHelper.Pi;
        associatedKey = Keys.Down;
    }

    public new void CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState)
    {
        foreach (Beat beat in activeBeats)
        {
            if (beat is BeatUp && targetArea.Intersects(beat.GetBoundingBox()))
            {
                activeBeats.Remove(beat);
                break;
            }
        }
    }
}