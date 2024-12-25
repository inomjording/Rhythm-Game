using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using RhythmGame.Beats;

namespace RhythmGame.BeatButtons;

internal class BeatButtonUp : BeatButton
{
    public BeatButtonUp(Texture2D texture) : base(texture)
    {
        Position = BeatGame.Origin - new Vector2(0, 50);
        Rotation = 0f;
        AssociatedKey = Keys.Up;
        Center = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        TargetArea = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }

    protected override List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState)
    {
        return activeBeats.Where(beat => beat is not BeatUp || !TargetArea.Intersects(beat.GetBoundingBox())).ToList();
    }
}
