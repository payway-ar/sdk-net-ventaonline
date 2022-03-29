using Newtonsoft.Json;

namespace Decidir.Model
{
    public class TokenRequest
    {
        public string card_number { get; set; }
        public string card_expiration_month { get; set; }
        public string card_expiration_year { get; set; }
        public string card_holder_name { get; set; }
        public string card_holder_birthday { get; set; }
        public string card_holder_door_number { get; set; }
        public string security_code { get; set; }
        public CardHolderIdentification card_holder_identification { get; set; }
        public TokenFraudDetection fraud_detection { get; set; }

        public string ip_address { get; set; }

        public static string toJson(TokenRequest token)
        {
            return JsonConvert.SerializeObject(token, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

    }
}
