using System;
using Decidir.Model;
using Newtonsoft.Json;

namespace DecidirTest
{
    internal class CardData
    {
        public string card_number { get; internal set; }
        public string card_expiration_month { get; internal set; }
        public string card_expiration_year { get; internal set; }
        public string security_code { get; internal set; }

        internal static string toJson(CardData data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public string card_holder_name { get; internal set; }
        public CardHolderIdentification card_holder_identification { get; internal set; }

        internal static CardData toCardData(string expected)
        {
            throw new NotImplementedException();
        }
    }

    
}