using System;
using Newtonsoft.Json;

namespace Decidir.Model
{
    public class HistoryRequest
    {
        public string cuit { get; set; }
        public string site_id { get; set; }
        public string operation_id { get; set; }
        public string status { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
        public string platform { get; set; }
        public int? page { get; set; }
        public int? items_per_page { get; set; }
        public string description { get; set; }
        public string currency { get; set; }
        public long? establishment_number { get; set; }
        public double? max_amount { get; set; }
        public double? min_amount { get; set; }
        public int? installments { get; set; }
        public long? payment_method_id { get; set; }

        public static string toJson(Object history)
        {
            return JsonConvert.SerializeObject(history, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}