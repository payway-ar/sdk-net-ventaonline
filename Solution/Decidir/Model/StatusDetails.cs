namespace Decidir.Model
{
    public class StatusDetails
    {
        public string ticket { get; set; }
        public string card_authorization_code { get; set; }
        public string address_validation_code { get; set; }
        public StatusErrorDetails error { get; set; }

        public StatusDetails()
        {
            this.error = new StatusErrorDetails();
        }
    }
}
