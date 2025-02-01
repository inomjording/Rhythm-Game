using System.IO;
using Newtonsoft.Json;

namespace RhythmGame.OptionsContext;

public abstract class SettingsManager
{
    private const string SettingsFilePath = "UserData/user-settings.json";

    public static void SaveSettings(UserSettings settings)
    {
        var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(SettingsFilePath, json);
    }

    public static UserSettings LoadSettings()
    {
        if (!File.Exists(SettingsFilePath)) return new UserSettings(); // Return default settings if file doesn't exist
        
        var json = File.ReadAllText(SettingsFilePath);

        return JsonConvert.DeserializeObject<UserSettings>(json);
    }
}