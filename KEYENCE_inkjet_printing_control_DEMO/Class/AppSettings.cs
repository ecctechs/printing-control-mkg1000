using System;
using System.IO;
using Newtonsoft.Json;

public class AppSettings
{
    public string StatusLogPath { get; set; }

    private static readonly string settingsFilePath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "appsettings.json"
    );

    /// บันทึก path ลงใน appsettings.json
    public static void SaveAppSettings(string path)
    {
        var settings = new AppSettings
        {
            StatusLogPath = path
        };

        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(settingsFilePath, json);
    }

    /// โหลด path จาก appsettings.json
    public static string LoadAppSettings()
    {
        if (!File.Exists(settingsFilePath))
        {
            return null;
        }

        string json = File.ReadAllText(settingsFilePath);
        var settings = JsonConvert.DeserializeObject<AppSettings>(json);
        return settings?.StatusLogPath;
    }
}
