namespace Decidir.Model
{
    public class CapturePaymentResponse : PaymentResponse
    {
        public ConfirmedCapturePayment confirmed { get; set; }

        public CapturePaymentResponse() : base()
        {
            this.confirmed = new ConfirmedCapturePayment();
        }
    }
}
