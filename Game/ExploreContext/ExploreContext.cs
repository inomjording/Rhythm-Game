using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.CharacterSprite;

namespace RhythmGame.ExploreContext;

public class ExploreContext(ContentManager content) : IGameContext
{
    private Texture2D background;
    private Texture2D foreground;
    private Vector2 backgroundPosition;

    private AnimatedCharacter playerCharacter;
    private Vector2 characterPosition;

    private SoundEffect song;
    private SoundEffectInstance songInstance;
    
    public bool ReturnToMainMenu { get; set; }
    public void LoadContent()
    {
        background = content.Load<Texture2D>("bgs/bg-home");
        foreground = content.Load<Texture2D>("bgs/fg-home");
        backgroundPosition = Vector2.Zero;

        characterPosition = new Vector2(300f, 260f);
        playerCharacter = new AnimatedCharacter(0.1f, characterPosition);
        playerCharacter.AddAnimation(new SpriteAnimation("Idle", content.Load<Texture2D>("character-sprites/mc/idle-char")));
        playerCharacter.AddAnimation(new SpriteAnimation("Walk", content.Load<Texture2D>("character-sprites/mc/walk")));
        playerCharacter.SetAnimation("Idle");
        
        song = content.Load<SoundEffect>("sound/songs/A Moment of Calmness");
        songInstance = song.CreateInstance();
        songInstance.IsLooped = true;
        songInstance.Play();
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            if (characterPosition.X > 300f)
            {
                characterPosition.X -= 4;
                playerCharacter.Position = characterPosition;
            }
            else if (backgroundPosition.X <= 0)
            {
                backgroundPosition.X += 4;
            } 
            else if (characterPosition.X >= -50f)
            {
                characterPosition.X -= 4;
                playerCharacter.Position = characterPosition;
            }
            if (playerCharacter.CurrentAnimation != "Walk")
            {
                playerCharacter.SetAnimation("Walk");
            }
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            if (characterPosition.X < 300f)
            {
                characterPosition.X += 4;
                playerCharacter.Position = characterPosition;
            }
            else if (backgroundPosition.X >= -2412f)
            {
                backgroundPosition.X -= 4;
            } 
            else if (characterPosition.X <= 458)
            {
                characterPosition.X += 4;
                playerCharacter.Position = characterPosition;
            }
            if (playerCharacter.CurrentAnimation != "Walk")
            {
                playerCharacter.SetAnimation("Walk");
            }
        }
        else
        {
            if (playerCharacter.CurrentAnimation != "Idle")
            {
                playerCharacter.SetAnimation("Idle");
            }
        }
        playerCharacter.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, backgroundPosition, Color.White);
        
        playerCharacter.Draw(spriteBatch);
        
        spriteBatch.Draw(foreground, backgroundPosition, Color.White);
    }
}