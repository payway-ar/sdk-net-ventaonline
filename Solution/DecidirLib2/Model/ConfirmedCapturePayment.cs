using System;

namespace Decidir.Model
{
    public class ConfirmedCapturePayment
    {
        public int id { get; set; }
        public int origin_amount { get; set; }
        public DateTime date { get; set; }
    }
}
