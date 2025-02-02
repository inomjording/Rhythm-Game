using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.CharacterSprite;

public class SpriteAnimation
{
    public readonly string Name;
    public readonly List<Rectangle> Frames;
    public readonly Texture2D Texture;
    public readonly bool Loop;
    public readonly SpriteEffects SpriteEffects;

    public SpriteAnimation(string name, string filename, ContentManager content, int? frameWidth = null, bool loop = true,
        SpriteEffects spriteEffects = SpriteEffects.None)
    {
        Name = name;
        Texture = content.Load<Texture2D>(filename);
        Frames = GenerateFrames(Texture, frameWidth);
        Loop = loop;
        SpriteEffects = spriteEffects;
    }

    public SpriteAnimation(string name, Texture2D texture, int? frameWidth = null,
        bool loop = true, SpriteEffects spriteEffects = SpriteEffects.None)
    {
        Name = name;
        Texture = texture;
        Frames = GenerateFrames(Texture, frameWidth);
        Loop = loop;
        SpriteEffects = spriteEffects;
    }
    
    private static List<Rectangle> GenerateFrames(Texture2D texture, int? frameWidth)
    {
        var frameHeight = texture.Height;
        frameWidth ??= frameHeight;
        
        var frames = new List<Rectangle>();
        for (var i = 0; i < texture.Width / frameWidth; i++)
        {
            frames.Add(new Rectangle((int)(i * frameWidth), 0, (int)frameWidth, frameHeight));
        }

        return frames;
    }
}