namespace RhythmGame.ScoreContext;

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class ScoreScreenManager
{
    private const string ScoreFilePath = "scores.json";

    public static List<Score> LoadScores()
    {
        if (!File.Exists(ScoreFilePath))
        {
            return [];
        }

        var json = File.ReadAllText(ScoreFilePath);
        return JsonConvert.DeserializeObject<List<Score>>(json) ?? [];
    }

    private static void SaveScores(List<Score> scores)
    {
        var json = JsonConvert.SerializeObject(scores, Formatting.Indented);
        File.WriteAllText(ScoreFilePath, json);
    }

    public static void AddScore(Score newScore)
    {
        var scores = LoadScores();
        scores.Add(newScore);
        scores.Sort((a, b) => b.Points.CompareTo(a.Points)); // Sort by highest points
        SaveScores(scores);
    }
}
