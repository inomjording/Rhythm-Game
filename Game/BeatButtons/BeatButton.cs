using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.Beats;

namespace RhythmGame.BeatButtons;

public class BeatButton
{
    // Fields
    private readonly Texture2D texture;
    protected Vector2 Position;
    protected float Rotation;
    protected Vector2 Center;
    private readonly float scale;
    private readonly SpriteEffects spriteEffects;
    private readonly Color defaultColor;
    private readonly Color activeColor;
    private Color currentColor;
    protected Keys AssociatedKey;
    protected Rectangle TargetArea;

    protected BeatButton(Texture2D texture) 
    {
        scale = 1f;
        this.texture = texture;
        spriteEffects = SpriteEffects.None;
        defaultColor = Color.White; // Default color
        activeColor = Color.Red;    // Active color when key is pressed
        currentColor = defaultColor;
        TargetArea = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        Center = new Vector2(TargetArea.Center.X, TargetArea.Center.Y);
    }

    // Update Method
    public List<Beat> Update(GameTime gameTime, KeyboardState keyboardState, KeyboardState oldState, List<Beat> activeBeats)
    {
        if (keyboardState.IsKeyDown(AssociatedKey))
        {
            currentColor = activeColor; // Change to active color
            if (oldState.IsKeyDown(AssociatedKey))
            {
                return activeBeats;
            }
            var beats = CheckForCollisions(activeBeats, keyboardState);
            return beats;
        }
        currentColor = defaultColor; // Revert to default color
        return activeBeats;
    }

    // Draw Method
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, Position, null, currentColor, Rotation, Center, scale, spriteEffects, 0f);
    }

    protected virtual List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState)
    {
        return [];
    }

    protected float CollisionMeasureX(Beat beat)
    {
        var boundingBox = beat.GetBoundingBox();
        var distance = decimal.Abs(boundingBox.Center.X - TargetArea.Center.X);
        return (float)(2 * distance / TargetArea.Width);
    }

    protected float CollisionMeasureY(Beat beat)
    {
        var boundingBox = beat.GetBoundingBox();
        var distance = decimal.Abs(boundingBox.Center.Y - TargetArea.Center.Y); // What's the difference between decimal etc.
        return (float)(2 * distance / TargetArea.Height);
    }
}
