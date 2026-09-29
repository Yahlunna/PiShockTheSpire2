namespace PiShockTheSpire2.PiShockTheSpire2Code.Utils;

public static class ShockUtil
{
    // Do a shock/vibrate/beep operation for a select shocker.
    public static Task DoOperationAsync(Op op, IEnumerable<string> shockerIds, TimeSpan duration, int intensity = 0)
    {
        if (Config.VibrateOnly)
        {
            op = Op.Buzz;
        }

        if (!Config.IsValid())
        {
            throw (new Exception("Invalid configuration settings Error! Review your mod configuration menú"));
        }
        
        intensity = RefineIntensity(intensity);
        duration  = RefineDuration(duration);

        IShockBackend backend;
        try
        {
            backend = GetBackend();
        }
        catch (Exception e)
        {
            MainFile.Logger.Error("Failed to create backend! " + e.Message);
            throw;
        }
        
        MainFile.Logger.Info($"{backend.BackendName}: sending {op} for {duration.TotalMilliseconds}ms with intensity {intensity}");
        try
        {
            _ = backend.DoOperationAsync(op, shockerIds, duration, intensity);
            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            MainFile.Logger.Error($"{backend.BackendName} Error! {e.Message}");
            throw;
        }
    }
    
    // Do a shock/vibrate/beep operation for all shockers.
    public static Task DoOperationForAllAsync(Op op, TimeSpan duration, int intensity = 0)
    {
        _ = DoOperationAsync(op, Config.GetAllShockerIds(), duration, intensity);
        return Task.CompletedTask;
    }

    private static IShockBackend GetBackend()
    {
        // Because PiShock and OpenShock API keys are different lengths, we can auto-detect which backend to use.
        return Config.API_Key.Length switch
        {
            32 or 36 => new PiShockApiHandler(), // PiShock UUID, with or without the 4 dashes.
            64 => new OpenShockApiHandler(), // OpenShock Token.
            _ => throw new Exception("Unable to determine backend from API Key's lenght. Are you sure the API Key is correct?")
        };
    }
    
    private static TimeSpan RefineDuration(TimeSpan duration)
    {
        if (duration < TimeSpan.FromSeconds(Config.MinDuration))
            return TimeSpan.FromSeconds(Config.MinDuration);
        if (duration > TimeSpan.FromSeconds(Config.MaxDuration))
            return TimeSpan.FromSeconds(Config.MaxDuration);

        return duration;
    }

    private static int RefineIntensity(int intensity)
    {
        if (intensity < (int)Config.MinIntensity) return (int)Config.MinIntensity;
        if (intensity > (int)Config.MaxIntensity) return (int)Config.MaxIntensity;
        
        return intensity;
    }
}

public enum Op
{
    Zap,
    Buzz,
    Beep,
    Stop,
}