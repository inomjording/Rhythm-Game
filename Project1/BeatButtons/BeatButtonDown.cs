using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame.Beats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RythmGame.BeatButtons
{
    internal class BeatButtonDown : BeatButton
    {
        public BeatButtonDown(Texture2D texture) : base(texture)
        {
            position = Game1.origin + new Vector2(0, 50);
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
}
