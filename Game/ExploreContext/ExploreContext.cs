using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RhythmGame.CharacterSprite;
using RhythmGame.Collections;

namespace RhythmGame.ExploreContext;

public class ExploreContext(ContentManager content, FontCollection fonts) : IGameContext
{
    private readonly string backgroundName = "home";
    private Texture2D background;
    private Texture2D foreground;
    private Vector2 backgroundPosition;
    
    private KeyboardState lastKeyboardState;
    
    private readonly List<InteractableObject> interactableObjects = [];
    private InteractableObject currentInteractableObject;

    private AnimatedCharacter playerCharacter;
    private Vector2 playerSpritePosition;

    private int playerPosition = 300;

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

    public ExploreContext(ContentManager content, FontCollection fonts, string backgroundName, float minXBackground, string songName) : this(content, fonts)
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
        
        interactableObjects.Add(new InteractableObject("Wardrobe", 110, 250, ["A test text.", "For the wardrobe.", "..."]));
        interactableObjects.Add(new InteractableObject("Fridge", 930, 1020, ["It's pretty empty.", "I'm getting my deliveries on Sundays."]));
        interactableObjects.Add(new InteractableObject("Mess", 1020, 1215, ["...", "Don't force me to take care of that now..."]));
        interactableObjects.Add(new InteractableObject("Bookcase", 1779, 1885, ["I don't know what I want to read."]));
        interactableObjects.Add(new InteractableObject("Computer", 2395, 2669, ["My trusted old computer.", "I keep it alive, and it repays the favor."]));
        interactableObjects.Add(new InteractableObject("Bed", 2872, 2888, ["I shouldn't sleep now.", "...", "...", "... I would like to, though."]));

        playerSpritePosition = new Vector2(300f, 260f);
        playerCharacter = new AnimatedCharacter(0.1f, playerSpritePosition);
        playerCharacter.AddAnimation(new SpriteAnimation("IdleLeft", content.Load<Texture2D>("character-sprites/mc/idle-char"), spriteEffects: SpriteEffects.FlipHorizontally));
        playerCharacter.AddAnimation(new SpriteAnimation("IdleRight", content.Load<Texture2D>("character-sprites/mc/idle-char")));
        playerCharacter.AddAnimation(new SpriteAnimation("WalkLeft", content.Load<Texture2D>("character-sprites/mc/walk"), spriteEffects: SpriteEffects.FlipHorizontally));
        playerCharacter.AddAnimation(new SpriteAnimation("WalkRight", content.Load<Texture2D>("character-sprites/mc/walk")));
        playerCharacter.SetAnimation("IdleRight");
        
        var song = content.Load<SoundEffect>("sound/songs/" + songName);
        songInstance = song.CreateInstance();
        songInstance.IsLooped = true;
        songInstance.Play();
    }

    public void Update(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Up))
        {
            Console.WriteLine(playerPosition);
        }
        
        FindInteractableObjects();
        if (currentInteractableObject != null)
        {
            currentInteractableObject.Update();
            if (keyboardState.IsKeyDown(Keys.Enter) && !lastKeyboardState.IsKeyDown(Keys.Enter))
            {
                currentInteractableObject.UpdateText();
            }
        }
        HandleMovement(keyboardState);
        playerCharacter.Update(gameTime);
        lastKeyboardState = keyboardState;
    }

    private void HandleMovement(KeyboardState keyboardState)
    {
        var animation = "IdleRight";

        if (keyboardState.IsKeyDown(Keys.Left))
        {
            MoveLeft();
            animation = "WalkLeft";
            currentInteractableObject?.Reset();
        }
        else if (keyboardState.IsKeyDown(Keys.Right))
        {
            MoveRight();
            animation = "WalkRight";
            currentInteractableObject?.Reset();
        }
        else if (playerCharacter.CurrentAnimation is "WalkLeft" or "IdleLeft")
        {
            animation = "IdleLeft";
        }

        if (playerCharacter.CurrentAnimation != animation)
        {
            playerCharacter.SetAnimation(animation);
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

    private void FindInteractableObjects()
    {
        foreach (var interactableObject in interactableObjects.Where(interactableObject => interactableObject.IsWithinBorder(playerPosition)))
        {
            currentInteractableObject = interactableObject;
            return;
        }
        currentInteractableObject?.Reset();
        currentInteractableObject = null;
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(background, backgroundPosition, Color.White);
        playerCharacter.Draw(spriteBatch);
        spriteBatch.Draw(foreground, backgroundPosition, Color.White);
        currentInteractableObject?.Draw(spriteBatch, fonts);
    }
}
