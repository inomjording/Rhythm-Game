using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.Beats;

namespace RhythmGame.BeatButtons;

public class BeatButton
{
    // Fields
    public Texture2D texture;
    public Vector2 position;
    public float rotation;
    public Vector2 center;
    public float scale;
    private SpriteEffects spriteEffects;
    private Color defaultColor;
    private Color activeColor;
    private Color currentColor;
    public Keys associatedKey;
    public Rectangle targetArea;

    // Constructor
    public BeatButton(Texture2D texture,
        Vector2 position,
        Keys associatedKey,
        float rotation = 0f, 
        float scale = 1f)
    {
        this.texture = texture;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        spriteEffects = SpriteEffects.None;
        defaultColor = Color.White; // Default color
        activeColor = Color.Red;    // Active color when key is pressed
        currentColor = defaultColor;
        center = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        targetArea = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        this.associatedKey = associatedKey;
    }

    public BeatButton(Texture2D texture) 
    {
        scale = 1f;
        this.texture = texture;
        spriteEffects = SpriteEffects.None;
        defaultColor = Color.White; // Default color
        activeColor = Color.Red;    // Active color when key is pressed
        currentColor = defaultColor;
        center = new Vector2(position.X + texture.Width / 2f, position.Y + texture.Height / 2f); // center origin by default
        targetArea = new Rectangle((int)(center.X - texture.Width * scale / 2), (int)(center.Y - texture.Height * scale / 2), (int)(texture.Width * scale), (int)(texture.Height * scale));
    }

    // Update Method
    public List<Beat> Update(GameTime gameTime, KeyboardState keyboardState, List<Beat> activeBeats)
    {
        if (keyboardState.IsKeyDown(associatedKey))
        {
            currentColor = activeColor; // Change to active color
            List<Beat> beats = CheckForCollisions(activeBeats, keyboardState);
            return beats;
        }
        currentColor = defaultColor; // Revert to default color
        return activeBeats;
    }

    // Draw Method
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, null, currentColor, rotation, center, scale, spriteEffects, 0f);
    }

    // Properties for Customization
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public float Rotation
    {
        get { return rotation; }
        set { rotation = value; }
    }

    public virtual List<Beat> CheckForCollisions(List<Beat> activeBeats, KeyboardState keyboardState)
    {
        return new List<Beat>();
    }
}