using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.CharacterSprite;

public class AnimatedCharacter(
    Dictionary<string, Texture2D> animationTextures,
    float frameInterval,
    Vector2 startPosition,
    float scale = 1f)
{
    private readonly Dictionary<string, List<Rectangle>> animations = new();
    private string currentAnimation = "Idle"; // Default animation
    private int currentFrame;
    private float frameTimer;
    public float FrameInterval = frameInterval;

    private Vector2 Position { get; } = startPosition;
    private float Scale { get; } = scale;

    public void AddAnimation(string name, List<Rectangle> frames)
    {
        animations[name] = frames;
    }

    public void SetAnimation(string name)
    {
        if (currentAnimation == name) return;
        currentAnimation = name;
        currentFrame = 0;
        frameTimer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!(frameTimer >= FrameInterval)) return;
        frameTimer = 0f;
        currentFrame++;

        if (currentFrame < animations[currentAnimation].Count) return;
        if (currentAnimation == "Idle")
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
        if (!animations.TryGetValue(currentAnimation, out var value)) return;
        var sourceRectangle = value[currentFrame];
        spriteBatch.Draw(animationTextures[currentAnimation],
            Position,
            sourceRectangle,
            Color.White,
            0f,
            Vector2.Zero,
            Scale,
            SpriteEffects.None,
            0f);
    }
}
