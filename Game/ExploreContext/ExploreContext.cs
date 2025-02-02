using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.CharacterSprite;

namespace RhythmGame.ExploreContext;

public class ExploreContext(ContentManager content) : IGameContext
{
    private readonly string backgroundName = "home";
    private Texture2D background;
    private Texture2D foreground;
    private Vector2 backgroundPosition;

    private AnimatedCharacter playerCharacter;
    private Vector2 playerSpritePosition;

    private int playerPosition = 300; // TODO: Add interactive objects relative to player position

    private readonly string songName = "A Moment of Calmness";
    private SoundEffectInstance songInstance;
    
    private const int MoveSpeed = 4;
    private const float CharacterXPanLeft = 100f;
    private const float CharacterXPanRight = 500f;
    private const float MinXCharacter = -50f;
    private const float MaxXCharacter = 500f;
    private readonly float minXBackground = -2375f;
    private const float MaxXBackground = 0f;
    
    public bool ReturnToMainMenu { get; set; }

    public ExploreContext(ContentManager content, string backgroundName, float minXBackground, string songName) : this(content)
    {
        this.backgroundName = backgroundName;
        this.minXBackground = minXBackground;
        this.songName = songName;
    }

    public void LoadContent()
    {
        background = content.Load<Texture2D>("bgs/bg-" + backgroundName);
        foreground = content.Load<Texture2D>("bgs/fg-" + backgroundName);
        backgroundPosition = Vector2.Zero;

        playerSpritePosition = new Vector2(300f, 260f);
        playerCharacter = new AnimatedCharacter(0.1f, playerSpritePosition);
        playerCharacter.AddAnimation(new SpriteAnimation("Idle", content.Load<Texture2D>("character-sprites/mc/idle-char")));
        playerCharacter.AddAnimation(new SpriteAnimation("Walk", content.Load<Texture2D>("character-sprites/mc/walk")));
        playerCharacter.SetAnimation("Idle");
        
        var song = content.Load<SoundEffect>("sound/songs/" + songName);
        songInstance = song.CreateInstance();
        songInstance.IsLooped = true;
        songInstance.Play();
    }

    public void Update(GameTime gameTime)
    {
        HandleMovement();
        playerCharacter.Update(gameTime);
    }

    private void HandleMovement()
    {
        var keyboardState = Keyboard.GetState();
        var isMoving = false;

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            MoveLeft();
            isMoving = true;
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            MoveRight();
            isMoving = true;
        }

        // Set animation only if it changes
        var targetAnimation = isMoving ? "Walk" : "Idle";
        if (playerCharacter.CurrentAnimation != targetAnimation)
        {
            playerCharacter.SetAnimation(targetAnimation);
        }
    }

    private void MoveLeft()
    {
        if (playerSpritePosition.X > CharacterXPanLeft)
        {
            playerSpritePosition.X -= MoveSpeed;
            playerPosition -= MoveSpeed;
        }
        else if (backgroundPosition.X < MaxXBackground)
        {
            backgroundPosition.X += MoveSpeed;
            playerPosition -= MoveSpeed;
        }
        else if (playerSpritePosition.X >= MinXCharacter)
        {
            playerSpritePosition.X -= MoveSpeed;
            playerPosition -= MoveSpeed;
        }
        playerCharacter.Position = playerSpritePosition;
    }

    private void MoveRight()
    {
        if (playerSpritePosition.X < CharacterXPanRight)
        {
            playerSpritePosition.X += MoveSpeed;
            playerPosition += MoveSpeed;
        }
        else if (backgroundPosition.X > minXBackground)
        {
            backgroundPosition.X -= MoveSpeed;
            playerPosition += MoveSpeed;
        }
        else if (playerSpritePosition.X <= MaxXCharacter)
        {
            playerSpritePosition.X += MoveSpeed;
            playerPosition += MoveSpeed;
        }
        playerCharacter.Position = playerSpritePosition;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, backgroundPosition, Color.White);
        playerCharacter.Draw(spriteBatch);
        spriteBatch.Draw(foreground, backgroundPosition, Color.White);
    }
}
