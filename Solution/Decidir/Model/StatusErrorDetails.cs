namespace Decidir.Model
{
    public class StatusErrorDetails
    {
        public string type { get; set; }
        public StatusErrorReason reason { get; set; }

        public StatusErrorDetails()
        {
            this.reason = new StatusErrorReason();
        }
    }
}
