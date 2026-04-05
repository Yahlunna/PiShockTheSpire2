using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using static PiShockTheSpire2.PiShockTheSpire2Code.Config;

namespace PiShockTheSpire2.PiShockTheSpire2Code.Utils;

public static class PiShockApiHandler
{
    private static readonly HttpClient _client = new HttpClient();
    
    public static async Task PostShockerOpAsync(int operation, int duration, int intensity)
    {
        try
        {
            duration = RefineDuration(duration);
            intensity = RefineIntensity(intensity);
            
            string piShockUrl = "https://api.pishock.com/Shockers/" + Config.Shocker_ID;
            
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("accept", "*/*");
            _client.DefaultRequestHeaders.Add("X-PiShock-Api-Key", Config.API_Key);
            _client.DefaultRequestHeaders.Add("X-PiShock-Username", Config.Username);

            var payload = new
            {
                AgentName = "PiShockTheSpire2",
                Operation = operation,
                Duration = duration,
                Intensity = intensity,
                IntensityAsPercentage = true,
            };
            
            HttpResponseMessage response = await _client.PostAsJsonAsync(piShockUrl, payload);
            
            // PiShock The Spire API Log:
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            int statusCode = (int)response.StatusCode;
            string statusCodeType = response.StatusCode.ToString();
            
            MainFile.Logger.Info("Request Success! Status code " + statusCode + ": " + statusCodeType + " -> " + responseString);
            
        }
        catch (Exception ex)
        {
            MainFile.Logger.Warn(ex.Message);
            if (ex.StackTrace != null) MainFile.Logger.Warn(ex.StackTrace);
        }
    }
    
    private static int RefineDuration(int duration)
    {
        if (duration < 1) return 1000;
        if (duration > (int)Config.MaxDuration) return ((int) MaxDuration * 1000);

        return (duration * 1000);
    }

    private static int RefineIntensity(int intensity)
    {
        if(intensity < 1) return 1;
        if (intensity > (int)Config.MaxIntensity) return (int) MaxIntensity;
        
        return intensity;
    }
    

}