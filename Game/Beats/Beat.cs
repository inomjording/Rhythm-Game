using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RhythmGame.Beats;

public class Beat
{
    public char BeatCode;
    private readonly Texture2D texture;
    protected Vector2 Position;
    protected Vector2 Direction;
    private readonly float speed;
    private readonly Vector2 origin = BeatGame.Origin;
    private readonly float scale;
    protected float Rotation;
    private readonly Color color;

    // Constructor
    protected Beat(Texture2D texture, float speed = 250f, float scale = 1f, Color color = default)
    {
        this.texture = texture;
        this.speed = speed;
        this.scale = scale;
        this.color = color;
    }

    // Update Method
    public void Update(GameTime gameTime, ScoreManager scoreManager)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Move towards center
        Position += Direction * speed * deltaTime;

        // Check if the beat has reached the center
        if (Vector2.Distance(Position, origin) < speed * deltaTime)
        {
            // Handle beat reaching the center (e.g., trigger an event, remove beat, etc.)
            // For now, we simply stop the beat at the center
            Position = origin;
            scoreManager.QueueHit(10f);
        }
    }

    // Draw Method
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture,
            Position,
            null,
            color,
            Rotation,
            new Vector2(texture.Width / 2f,
                texture.Height / 2f),
            scale,
            SpriteEffects.None,
            0f);
    }

    // Optional: A property or method to check if the beat has reached the center
    public bool HasReachedCenter()
    {
        return Position == origin;
    }

    // Method to get the bounding box of the beat
    public Rectangle GetBoundingBox()
    {
        return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
    }
}
