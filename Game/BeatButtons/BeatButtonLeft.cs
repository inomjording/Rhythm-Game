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
        Position = BeatGame.Origin - new Vector2(50, 0);
        Rotation = -MathHelper.PiOver2;
        AssociatedKey = Keys.Left;
        Center = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        TargetArea = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }

    protected override List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState)
    {
        return activeBeats.Where(beat => beat is not BeatLeft || !TargetArea.Intersects(beat.GetBoundingBox())).ToList();
    }
}
