using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RhythmGame.OptionsContext;

public static class SettingsManager
{
    private static readonly string SettingsFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "RhythmGame",
        "user-settings.json"
    );

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public static void SaveSettings(UserSettings settings)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SettingsFilePath)!);
        File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(settings, SerializerOptions));
    }

    public static UserSettings LoadSettings() =>
        File.Exists(SettingsFilePath)
            ? JsonSerializer.Deserialize<UserSettings>(File.ReadAllText(SettingsFilePath), SerializerOptions)
            : new();
}