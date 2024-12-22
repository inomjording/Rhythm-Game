using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using RhythmGame.Beats;

namespace RhythmGame.BeatButtons;

internal class BeatButtonLeft : BeatButton
{
    public BeatButtonLeft(Texture2D texture) : base(texture)
    {
        position = BeatGame.origin - new Vector2(50, 0);
        rotation = -MathHelper.PiOver2;
        associatedKey = Keys.Left;
        center = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        targetArea = new Rectangle((int)(position.X - center.X * scale), (int)(position.Y - center.Y * scale), (int)(texture.Width * scale), (int)(texture.Height * scale));
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