using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1;
using Project1.Beats;

public class BeatManager
{
    private List<(List<Beat>, float)> beatQueue; // List of beats and their spawn times
    private float beatInterval; // Time between beats (in seconds)
    private Vector2 center;
    private Texture2D beatTexture;
    private float speed;

    public BeatManager(float beatInterval, Vector2 center, Texture2D beatTexture, float speed)
    {
        this.beatInterval = beatInterval;
        this.center = Game1.origin;
        this.beatTexture = beatTexture;
        this.beatQueue = new List<(List<Beat>, float)>();
        this.speed = speed;
    }

    // Method to load beats from a text file
    public void LoadBeatsFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        float currentTime = 0f; // Start at 0 seconds
        foreach (var line in lines)
        {
            var beats = new List<Beat>();
             
            // Check each character in the line
            foreach (char direction in line)
            {
                switch (direction)
                {
                    case 'U':
                        beats.Add(new BeatUp(beatTexture, speed));
                        break;
                    case 'D':
                        beats.Add(new BeatDown(beatTexture, speed));
                        break;
                    case 'L':
                        beats.Add(new BeatLeft(beatTexture, speed));
                        break;
                    case 'R':
                        beats.Add(new BeatRight(beatTexture, speed));
                        break;
                }
            }

            // Add the beats and their corresponding spawn time to the queue
            beatQueue.Add((beats, currentTime));
            currentTime += beatInterval; // Move to the next beat time
        }
    }

    // Method to update and spawn beats based on time
    public void Update(GameTime gameTime, List<Beat> activeBeats)
    {
        float elapsedTime = (float)gameTime.TotalGameTime.TotalSeconds;

        // Spawn beats at the correct time
        for (int i = beatQueue.Count - 1; i >= 0; i--)
        {
            if (elapsedTime >= beatQueue[i].Item2 - 0.02f)
            {
                // Add the beats to the active beats list
                activeBeats.AddRange(beatQueue[i].Item1);

                // Remove from the queue after spawning
                beatQueue.RemoveAt(i);
            }
        }

        // Update all active beats
        foreach (var beat in activeBeats)
        {
            beat.Update(gameTime);
        }

        // Remove any beats that have reached the center
        activeBeats.RemoveAll(beat => beat.HasReachedCenter());
    }

    // Method to draw active beats
    public void Draw(SpriteBatch spriteBatch, List<Beat> activeBeats)
    {
        foreach (var beat in activeBeats)
        {
            beat.Draw(spriteBatch);
        }
    }
}
