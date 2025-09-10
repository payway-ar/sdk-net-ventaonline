namespace Decidir.Model
{
    public class RefundResponse
    {
        public long? id { get; set; }
        public long amount { get; set; }
        private CardError error { get; set; }
        public string status { get; set; }

        public StatusDetails status_details { get; set; }
        public string tid { get; set; }
    }
}
