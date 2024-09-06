using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RythmGame;
using RythmGame.Beats;

public class BeatManager
{
    // Separate queues for each direction
    private Queue<(Beat, float)> upQueue = new Queue<(Beat, float)>();
    private Queue<(Beat, float)> downQueue = new Queue<(Beat, float)>();
    private Queue<(Beat, float)> leftQueue = new Queue<(Beat, float)>();
    private Queue<(Beat, float)> rightQueue = new Queue<(Beat, float)>();

    private float beatInterval; // Time between beats (in seconds)
    private Vector2 center;
    private Texture2D beatTexture;
    private float speed;

    public BeatManager(float beatInterval, Vector2 center, Texture2D beatTexture, float speed)
    {
        this.beatInterval = beatInterval;
        this.center = Game1.origin;
        this.beatTexture = beatTexture;
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
                        upQueue.Enqueue((new BeatUp(beatTexture, speed), currentTime));
                        break;
                    case 'D':
                        downQueue.Enqueue((new BeatDown(beatTexture, speed), currentTime));
                        break;
                    case 'L':
                        leftQueue.Enqueue((new BeatLeft(beatTexture, speed), currentTime));
                        break;
                    case 'R':
                        rightQueue.Enqueue((new BeatRight(beatTexture, speed), currentTime));
                        break;
                }
            }

            currentTime += beatInterval; // Move to the next beat time
        }
    }

    // Helper method to spawn beats from a queue based on the elapsed time
    private void SpawnBeatsFromQueue(Queue<(Beat, float)> beatQueue, float elapsedTime, List<Beat> activeBeats)
    {
        while (beatQueue.Count > 0 && elapsedTime >= beatQueue.Peek().Item2)
        {
            var (beat, spawnTime) = beatQueue.Dequeue();
            activeBeats.Add(beat);
        }
    }

    // Method to update and spawn beats based on time
    public void Update(GameTime gameTime, List<Beat> activeBeats)
    {
        float elapsedTime = (float)gameTime.TotalGameTime.TotalSeconds;

        // Spawn beats for each direction queue
        SpawnBeatsFromQueue(upQueue, elapsedTime, activeBeats);
        SpawnBeatsFromQueue(downQueue, elapsedTime, activeBeats);
        SpawnBeatsFromQueue(leftQueue, elapsedTime, activeBeats);
        SpawnBeatsFromQueue(rightQueue, elapsedTime, activeBeats);

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
