using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.CharacterSprite;

public class AnimatedCharacter(
    float frameInterval,
    Vector2 startPosition,
    float scale = 1f)
{
    private readonly Dictionary<string, SpriteAnimation> animations = new();
    public string CurrentAnimation { get; private set; } = "Idle";
    private int currentFrame;
    private float frameTimer;
    public float FrameInterval = frameInterval;

    public Vector2 Position { private get; set; } = startPosition;
    private float Scale { get; } = scale;

    public void AddAnimation(SpriteAnimation animation)
    {
        animations[animation.Name] = animation;
    }

    public void SetAnimation(string name)
    {
        if (CurrentAnimation == name) return;
        CurrentAnimation = name;
        currentFrame = 0;
        frameTimer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!(frameTimer >= FrameInterval)) return;
        frameTimer = 0f;
        currentFrame++;

        if (currentFrame < animations[CurrentAnimation].Frames.Count) return;
        if (animations[CurrentAnimation].Loop)
        {
            currentFrame = 0; // Loop the animation
        }
        else
        {
            currentFrame--;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!animations.TryGetValue(CurrentAnimation, out var spriteAnimation)) return;
        var sourceRectangle = spriteAnimation.Frames[currentFrame];
        spriteBatch.Draw(animations[CurrentAnimation].Texture,
            Position,
            sourceRectangle,
            Color.White,
            0f,
            Vector2.Zero,
            Scale,
            spriteAnimation.SpriteEffects,
            0f);
    }
}
