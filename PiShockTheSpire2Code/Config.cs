using PiShockTheSpire2.PiShockTheSpire2Code.Utils;

namespace PiShockTheSpire2.PiShockTheSpire2Code;
using BaseLib.Config;
using BaseLib.Config.UI;
using Godot;

[HoverTipsByDefault]
public class Config : SimpleModConfig
{
    
    public static string Username { get; set; } = "undefined";
    public static string API_Key { get; set; } = "undefined";
    public static string Shocker_ID { get; set; } = "undefined";
    
    [SliderRange(1, 100)]
    public static double MaxIntensity { get; set; } = 100f;
    
    [SliderRange(1, 10)]
    public static double MaxDuration { get; set; } = 10f;

    public static bool AlwaysMaxPower { get; set; } = false;
    public static bool DeathPenalty { get; set; } = true;

    public override void SetupConfigUI(Control optionContainer)
    {
        GenerateOptionsForAllProperties(optionContainer);
        AddRestoreDefaultsButton(optionContainer);  
    }

    [ConfigButton("Vibrate")]
    public async Task SendVibrationTest()
    {
        await PiShockApiHandler.PostShockerOpAsync(1, (int)MaxDuration, (int)MaxIntensity);
    }

}