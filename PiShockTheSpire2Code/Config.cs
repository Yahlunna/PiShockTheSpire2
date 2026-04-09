using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code;
using BaseLib.Config;
using BaseLib.Config.UI;
using Godot;

[HoverTipsByDefault]
public class Config : SimpleModConfig
{

    public static bool SpawnShockerRelic { get; set; } = true;
    [ConfigSection("ApiConfig")] 
    public static string Username { get; set; } = "undefined";
    public static string API_Key { get; set; } = "undefined";
    public static string Shocker_ID { get; set; } = "undefined";
    
    [ConfigSection("ShockerConfig")]
    [SliderRange(1, 99)]
    public static double MinIntensity { get; set; } = 1f;
    [SliderRange(1, 100)]
    public static double MaxIntensity { get; set; } = 100f;
    [SliderRange(1, 9)]
    public static double MinDuration { get; set; } = 1f;
    [SliderRange(1, 10)]
    public static double MaxDuration { get; set; } = 10f;
    public static bool AlwaysMaxPower { get; set; } = false;
    
    [ConfigSection("Gameplay")]
    public static bool DeathPenalty { get; set; } = true;
    public static bool HealingVibrates { get; set; } = true;
    public static bool TriggerSelfDamage { get; set; } = true;
    public static bool AllowPunishments { get; set; } = true;

    [ConfigSection("Debug")]
    [ConfigButton("Vibrate")]
    public async Task SendVibrationTest()
    {
        await PiShockApiHandler.PostShockerOpAsync(1, (int)MaxDuration, (int)MaxIntensity);
    }

    public override void SetupConfigUI(Control optionContainer)
    {
        GenerateOptionsForAllProperties(optionContainer);
        AddRestoreDefaultsButton(optionContainer);  
    }
    
}