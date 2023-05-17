namespace Decidir.Model
{
    public class GetInternalTokenResponse
    {

        public string token_id { get; set; }
        public string brand { get; set; }
        public string card_type { get; set; }
        public string last_four_digits { get; set; }
        public string trace_id { get; set; }
        public string span_id { get; set; }
/*
        public string token_expiration_date { get; set; }
        public string token_state { get; set; }
        public string primary_scheme { get; set; }
        public string creation_timestamp { get; set; }*/


    }
}