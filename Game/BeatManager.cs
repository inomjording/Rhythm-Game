using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.Beats;

namespace RhythmGame;

public class BeatManager
{
    // Separate queues for each direction
    private readonly Queue<(Beat, float)> upQueue = new();
    private readonly Queue<(Beat, float)> downQueue = new();
    private readonly Queue<(Beat, float)> leftQueue = new();
    private readonly Queue<(Beat, float)> rightQueue = new();

    private readonly float beatInterval; // Time between beats (in seconds)
    private Vector2 center;
    private readonly Texture2D beatTexture;
    private readonly float speed;

    public BeatManager(float beatInterval, Vector2 center, Texture2D beatTexture, float speed)
    {
        this.beatInterval = beatInterval;
        this.center = BeatGame.Origin;
        this.beatTexture = beatTexture;
        this.speed = speed;
    }

    // Method to load beats from a text file
    public void LoadBeatsFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);

        var currentTime = 0f - 150f/speed; // compensate for time it takes to travel
        foreach (var line in lines)
        {
            var beatColor = Color.Cyan;
            if (line.Length > 1)
            {
                beatColor = Color.Magenta;
            }
             
            // Check each character in the line
            foreach (char direction in line)
            {
                switch (direction)
                {
                    case 'U':
                        upQueue.Enqueue((new BeatUp(beatTexture, speed, color: beatColor), currentTime));
                        break;
                    case 'D':
                        downQueue.Enqueue((new BeatDown(beatTexture, speed, color: beatColor), currentTime));
                        break;
                    case 'L':
                        leftQueue.Enqueue((new BeatLeft(beatTexture, speed, color: beatColor), currentTime));
                        break;
                    case 'R':
                        rightQueue.Enqueue((new BeatRight(beatTexture, speed, color: beatColor), currentTime));
                        break;
                }
            }

            currentTime += beatInterval; // Move to the next beat time
        }
    }

    // Helper method to spawn beats from a queue based on the elapsed time
    private static void SpawnBeatsFromQueue(Queue<(Beat, float)> beatQueue, float elapsedTime, List<Beat> activeBeats)
    {
        while (beatQueue.Count > 0 && elapsedTime >= beatQueue.Peek().Item2)
        {
            var (beat, spawnTime) = beatQueue.Dequeue();
            activeBeats.Add(beat);
        }
    }

    // Method to update and spawn beats based on time
    public void Update(GameTime gameTime, List<Beat> activeBeats, ScoreManager scoreManager)
    {
        var elapsedTime = (float)gameTime.TotalGameTime.TotalSeconds;

        // Spawn beats for each direction queue
        SpawnBeatsFromQueue(upQueue, elapsedTime, activeBeats);
        SpawnBeatsFromQueue(downQueue, elapsedTime, activeBeats);
        SpawnBeatsFromQueue(leftQueue, elapsedTime, activeBeats);
        SpawnBeatsFromQueue(rightQueue, elapsedTime, activeBeats);

        // Update all active beats
        foreach (var beat in activeBeats)
        {
            beat.Update(gameTime, scoreManager);
        }

        // Remove any beats that have reached the center
        activeBeats.RemoveAll(beat => beat.HasReachedCenter());
    }

    // Method to draw active beats
    public static void Draw(SpriteBatch spriteBatch, List<Beat> activeBeats)
    {
        foreach (var beat in activeBeats)
        {
            beat.Draw(spriteBatch);
        }
    }
}
