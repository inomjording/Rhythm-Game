﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.DanceContext.Beats;

public class BeatRight : Beat
{
    public BeatRight(Texture2D texture,
        float speed = 200,
        float scale = 1,
        Color color = default) : base(texture,
        speed,
        scale,
        color)
    {
        Position = new Vector2(DanceContext.Origin.X + SpawnDistanceFromOrigin, DanceContext.Origin.Y);
        Direction = Vector2.Normalize(DanceContext.Origin - Position);
        Rotation = MathHelper.PiOver2;
    }
}
