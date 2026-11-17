using System;
using Newtonsoft.Json;

namespace Decidir.Model
{
    public class TransactionHistoryRequest
    {
        public string charge_id { get; set; }

        public static string toJson(Object transactionHistory)
        {
            return JsonConvert.SerializeObject(transactionHistory, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}