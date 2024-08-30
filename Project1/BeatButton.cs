using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class BeatButton
{
    // Fields
    private Texture2D texture;
    private Vector2 position;
    private float rotation;
    private Vector2 origin;
    private float scale;
    private SpriteEffects spriteEffects;
    private Color defaultColor;
    private Color activeColor;
    private Color currentColor;
    private Keys associatedKey;

    // Constructor
    public BeatButton(Texture2D texture, Vector2 position, Keys associatedKey, float rotation = 0f, float scale = 1f)
    {
        this.texture = texture;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.spriteEffects = SpriteEffects.None;
        this.defaultColor = Color.White; // Default color
        this.activeColor = Color.Red;    // Active color when key is pressed
        this.currentColor = defaultColor;
        this.origin = new Vector2(texture.Width / 2f, texture.Height / 2f); // center origin by default
        this.associatedKey = associatedKey;
    }

    // Update Method
    public void Update(GameTime gameTime, KeyboardState keyboardState)
    {
        if (keyboardState.IsKeyDown(associatedKey))
        {
            currentColor = activeColor; // Change to active color
        }
        else
        {
            currentColor = defaultColor; // Revert to default color
        }
    }

    // Draw Method
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, null, currentColor, rotation, origin, scale, spriteEffects, 0f);
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
}
