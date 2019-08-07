using Newtonsoft.Json;
using System;

namespace Decidir.Model
{
    public class HealthCheckResponse
    {
        public string name { get; set; }
        public string version { get; set; }
        public string buildTime { get; set; }

        public static string toJson(HealthCheckResponse response)
        {
            return JsonConvert.SerializeObject(response, Newtonsoft.Json.Formatting.None);
        }

        public static HealthCheckResponse toHealthCheckResponse(string json)
        {
            HealthCheckResponse data;

            try
            {
                data = JsonConvert.DeserializeObject<HealthCheckResponse>(json);
            }
            catch (Exception)
            {
                data = null;
            }

            return data;
        }
    }
}
