using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.CharacterSprite;

public class AnimatedCharacter
{
    private Dictionary<string, Texture2D> animationTextures;
    private Dictionary<string, List<Rectangle>> animations;
    private string currentAnimation;
    private int currentFrame;
    private float frameTimer;
    public float frameInterval;

    public Vector2 Position { get; set; }
    public float Scale { get; set; }

    public AnimatedCharacter(Dictionary<string, Texture2D> animationTextures, float frameInterval, Vector2 startPosition, float scale = 1f)
    {
        this.animationTextures = animationTextures;
        this.frameInterval = frameInterval;
        this.Position = startPosition;
        this.Scale = scale;

        animations = new Dictionary<string, List<Rectangle>>();
        currentAnimation = "Idle"; // Default animation
        currentFrame = 0;
        frameTimer = 0f;
    }

    public void AddAnimation(string name, List<Rectangle> frames)
    {
        animations[name] = frames;
    }

    public void SetAnimation(string name)
    {
        if (currentAnimation != name)
        {
            currentAnimation = name;
            currentFrame = 0;
            frameTimer = 0f;
        }
    }

    public void Update(GameTime gameTime)
    {
        frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (frameTimer >= frameInterval)
        {
            frameTimer = 0f;
            currentFrame++;

            if (currentFrame >= animations[currentAnimation].Count)
            {
                if (currentAnimation == "Idle")
                {
                    currentFrame = 0; // Loop the animation
                }
                else
                {
                    currentFrame--;
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!animations.ContainsKey(currentAnimation)) return;
        var sourceRectangle = animations[currentAnimation][currentFrame];
        spriteBatch.Draw(animationTextures[currentAnimation], Position, sourceRectangle, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
    }
}
