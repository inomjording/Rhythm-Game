using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RhythmGame.CharacterSprite;

public class DancingCharacter
{
    private AnimatedCharacter animatedCharacter;
    private Dictionary<string, Texture2D> animationTextures;
    public DancingCharacter(Dictionary<string, Texture2D> animationTextures, Vector2 startPosition, float scale = 1f)
    {
        this.animationTextures = animationTextures;
        animatedCharacter = new AnimatedCharacter(animationTextures, 0.5f, startPosition, scale);

        // Add animations for each spritesheet
        foreach (var animation in animationTextures)
        {
            animatedCharacter.AddAnimation(animation.Key, GenerateFrames(animation.Value));
        }
    }
    
    private List<Rectangle> GenerateFrames(Texture2D texture)
    {
        int frameWidth = 74;
        int frameHeight = 76;

        List<Rectangle> frames = new List<Rectangle>();
        for (int i = 0; i < texture.Width / frameWidth; i++)
        {
            frames.Add(new Rectangle(i * frameWidth, 0, frameWidth, frameHeight));
        }

        return frames;
    }
    
    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
        animatedCharacter.frameInterval = 0.05f;
        // Change the animation based on the key pressed
        if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Right))
        {
            animatedCharacter.SetAnimation("DanceSplit");
        }
        else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Up))
        {
            animatedCharacter.SetAnimation("DanceLeftUp");
        }
        else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Up))
        {
            animatedCharacter.SetAnimation("DanceRightUp");
        }
        else if (keyboardState.IsKeyDown(Keys.Right) && keyboardState.IsKeyDown(Keys.Down))
        {
            animatedCharacter.SetAnimation("DanceRightDown");
        }
        else if (keyboardState.IsKeyDown(Keys.Left) && keyboardState.IsKeyDown(Keys.Down))
        {
            animatedCharacter.SetAnimation("DanceLeftDown");
        }
        else if (keyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyDown(Keys.Down))
        {
            animatedCharacter.SetAnimation("DanceUpDown");
        }
        else if (keyboardState.IsKeyDown(Keys.Up))
        {
            animatedCharacter.SetAnimation("DanceUp");
        }
        else if (keyboardState.IsKeyDown(Keys.Down))
        {
            animatedCharacter.SetAnimation("DanceDown");
        }
        else if (keyboardState.IsKeyDown(Keys.Left))
        {
            animatedCharacter.SetAnimation("DanceLeft");
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            animatedCharacter.SetAnimation("DanceRight");
        }
        else
        {
            animatedCharacter.SetAnimation("Idle");
            animatedCharacter.frameInterval = 0.2f;
        }

        animatedCharacter.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        animatedCharacter.Draw(spriteBatch);
    }
}
