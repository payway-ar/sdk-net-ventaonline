using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decidir.Model
{
    public class CardData
    {
        public string card_holder_name { get; set; }
        public string last_four_digits{ get; set; }
        public string card_holder_birthday { get; set; }
        public int card_holder_door_number { get; set; }
        public CardHolderIdentification card_holder_identification { get; set; }
        public string security_code { get; set; }  
        public string card_expiration_month { get; set; }
        public string card_expiration_year { get; set; }
        public string card_number { get; set; }

    }
}
