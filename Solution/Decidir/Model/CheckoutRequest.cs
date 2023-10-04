using System;
using Newtonsoft.Json;

namespace Decidir.Model
{
    public class CheckoutRequest
    {
        public string id { get; set; }
        public string origin_platform { get; set; }
        public string payment_description { get; set; }
        public CheckoutProductModel[] products { get; set; }
        public double? total_price { get; set; }
        public string site { get; set; }
        public int? template_id { get; set; }

        public int[] installments { get; set; }

        public Boolean plan_gobierno { get; set; }

        public string cancel_url { get; set; }

        public string success_url { get; set; }
        public string redirect_url { get; set; }

        public string notifications_url { get; set; }

        public long? life_time { get; set; }

        public int? id_payment_method { get; set; }

        public static string toJson(Object payment)
        {
            return JsonConvert.SerializeObject(payment, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }


    }


}
