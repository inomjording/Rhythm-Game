﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Beats
{
    public class BeatLeft : Beat
    {
        public BeatLeft(Texture2D texture, float speed = 200, float scale = 1) : base(texture, speed, scale)
        {
            position = new Vector2(0, Game1.origin.Y);
            direction = Vector2.Normalize(Game1.origin - position);
            rotation = -MathHelper.PiOver2;
        }
    }
}