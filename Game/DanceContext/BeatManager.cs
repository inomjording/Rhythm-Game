using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RhythmGame.DanceContext.Beats;

namespace RhythmGame.DanceContext;

public class BeatManager(Texture2D beatTexture)
{
    // Separate queues for each direction
    private readonly Queue<(Beat, float)> upQueue = new();
    private readonly Queue<(Beat, float)> downQueue = new();
    private readonly Queue<(Beat, float)> leftQueue = new();
    private readonly Queue<(Beat, float)> rightQueue = new();

    private float beatInterval; // Time between beats (in seconds)
    private float speed;

    // Method to load beats from a text file
    public void LoadBeatsFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        LoadSpeedAndInterval(lines[0]);

        var currentTime = 0f - 150f/speed; // compensate for time it takes to travel
        for (var i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var beatColor = Color.Cyan;
            if (line.Length > 1)
            {
                beatColor = Color.Magenta;
            }
             
            // Check each character in the line
            foreach (var direction in line)
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

    private void LoadSpeedAndInterval(string dataString)
    {
        var split = dataString.Split(',');
        beatInterval = 60f / float.Parse(split[0], CultureInfo.InvariantCulture) * 0.5f;
        speed = float.Parse(split[1], CultureInfo.CurrentCulture);
    }

    // Helper method to spawn beats from a queue based on the elapsed time
    private static List<Beat> SpawnBeatsFromQueue(Queue<(Beat, float)> beatQueue, float elapsedTime)
    {
        var beats = new List<Beat>();
        while (beatQueue.Count > 0 && elapsedTime >= beatQueue.Peek().Item2)
        {
            var (beat, _) = beatQueue.Dequeue();
            beats.Add(beat);
        }
        return beats;
    }

    // Method to update and spawn beats based on time
    public void Update(GameTime gameTime, float elapsedTime, List<Beat> activeBeats, ScoreManager scoreManager)
    {

        // Spawn beats for each direction queue
        activeBeats.AddRange(SpawnBeatsFromQueue(upQueue, elapsedTime));
        activeBeats.AddRange(SpawnBeatsFromQueue(downQueue, elapsedTime));
        activeBeats.AddRange(SpawnBeatsFromQueue(leftQueue, elapsedTime));
        activeBeats.AddRange(SpawnBeatsFromQueue(rightQueue, elapsedTime));

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
