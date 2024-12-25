using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Beats;

public class Beat
{
    public char beatCode;
    public Texture2D texture;
    public Vector2 position;
    public Vector2 direction;
    private float speed;
    private Vector2 origin = BeatGame.origin;
    public float scale;
    public float rotation = 0f;
    public Color color = Color.White;

    // Constructor
    public Beat(Texture2D texture, float speed = 250f, float scale = 1f, Color color = default)
    {
        this.texture = texture;
        this.speed = speed;
        this.scale = scale;
        this.color = color;
    }
    public Beat(Texture2D texture, Vector2 startPosition, float rotation = 0f, float speed = 200f, float scale = 1f)
    {
        this.texture = texture;
        position = startPosition;
        origin = new Vector2(200, 200);
        this.speed = speed;
        this.scale = scale;
        this.rotation = rotation;

        // Calculate direction towards center
        direction = Vector2.Normalize(origin - startPosition);
    }

    // Update Method
    public void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Move towards center
        position += direction * speed * deltaTime;

        // Check if the beat has reached the center
        if (Vector2.Distance(position, origin) < speed * deltaTime)
        {
            // Handle beat reaching the center (e.g., trigger an event, remove beat, etc.)
            // For now, we simply stop the beat at the center
            position = origin;
        }
    }

    // Draw Method
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, null, color, rotation, new Vector2(texture.Width / 2f, texture.Height / 2f), scale, SpriteEffects.None, 0f);
    }

    // Optional: A property or method to check if the beat has reached the center
    public bool HasReachedCenter()
    {
        return position == origin;
    }

    // Method to get the bounding box of the beat
    public Rectangle GetBoundingBox()
    {
        return new Rectangle((int)(position.X - origin.X * scale), (int)(position.Y - origin.Y * scale), (int)(texture.Width * scale), (int)(texture.Height * scale));
    }
}