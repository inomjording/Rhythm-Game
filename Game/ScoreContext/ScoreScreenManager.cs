namespace RhythmGame.ScoreContext;

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class ScoreScreenManager
{
    private const string ScoreFilePath = "scores.json";

    public static Dictionary<string, List<Score>> LoadScores()
    {
        if (!File.Exists(ScoreFilePath))
        {
            return [];
        }

        var json = File.ReadAllText(ScoreFilePath);
        return JsonConvert.DeserializeObject<Dictionary<string, List<Score>>>(json) ?? [];
    }

    private static void SaveScores(Dictionary<string, List<Score>> scores)
    {
        var json = JsonConvert.SerializeObject(scores, Formatting.Indented);
        File.WriteAllText(ScoreFilePath, json);
    }

    public static void AddScore(Score newScore, string songName)
    {
        var scores = LoadScores();
        scores[songName].Add(newScore);
        scores[songName].Sort((a, b) => b.Points.CompareTo(a.Points)); // Sort by highest points
        SaveScores(scores);
    }
}
