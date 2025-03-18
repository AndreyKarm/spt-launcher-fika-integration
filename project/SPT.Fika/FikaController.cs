using System.Diagnostics;
using System.Text.Json;
using SPT.Fika.Models;

namespace SPT.Fika
{
    public class FikaController
    {
        public static FikaPlayer[] GetOnlinePlayers()
        {
            try
            {
                string json = RequestHandler.RequestOnlinePlayers();
                return JsonSerializer.Deserialize<FikaPlayer[]>(json) ?? Array.Empty<FikaPlayer>();
            }
            catch
            {
                return Array.Empty<FikaPlayer>();
            }
        }

        public static FikaDedicatedData GetDedicatedData()
        {
            try
            {
                string json = RequestHandler.RequestDedicatedClientStatus();
                Debug.WriteLine("Received dedicated data JSON: " + json);

                // First deserialize as a list of dedicated status objects
                var statusList = JsonSerializer.Deserialize<List<DedicatedStatus>>(json);

                // Create and return a FikaDedicatedData object with the first status if available
                var result = new FikaDedicatedData();
                if (statusList != null && statusList.Count > 0)
                {
                    result.Available = statusList[0];
                }
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deserializing dedicated data: {ex.Message}");
                return new FikaDedicatedData();
            }
        }
    }
}