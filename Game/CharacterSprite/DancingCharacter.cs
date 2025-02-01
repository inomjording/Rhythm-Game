using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RhythmGame.CharacterSprite;

public class DancingCharacter
{
    private readonly AnimatedCharacter animatedCharacter;
    private readonly float idleFrameInterval;

    public DancingCharacter(Dictionary<string, Texture2D> animationTextures, Vector2 startPosition, float idleFrameInterval = 0.2f, float scale = 1f)
    {
        animatedCharacter = new AnimatedCharacter(0.5f, startPosition, scale);
        this.idleFrameInterval = idleFrameInterval;

        // Add animations for each spritesheet
        foreach (var animation in animationTextures)
        {
            var loop = animation.Key == "Idle";
            animatedCharacter.AddAnimation(new SpriteAnimation(animation.Key, animation.Value, 74, loop));
        }
    }
    
    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
        animatedCharacter.FrameInterval = 0.05f;
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
            animatedCharacter.FrameInterval = idleFrameInterval;
        }

        animatedCharacter.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        animatedCharacter.Draw(spriteBatch);
    }
}
