using Decidir;
using Decidir.Model;
using DecidirExample.Models;
using Microsoft.AspNetCore.Mvc;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace DecidirExample.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public ActionResult<PaymentResponse> Payment(PaymentDTO payment)
        {
            DecidirConnector decidir = new DecidirConnector(payment.AmbienteId, payment.privateApiKey, payment.publicApiKey);
            PaymentResponse respuesta = decidir.Payment(GetPayment(payment));

            return respuesta;
        }

        [HttpPost]
        public ActionResult<RefundResponse> Refund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);
            RefundResponse respuesta = decidir.Refund(paymentId);

            return respuesta;
        }

        [HttpPost]
        public ActionResult<DeleteRefundResponse> DeleteRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, long refundId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);
            DeleteRefundResponse respuesta = decidir.DeleteRefund(paymentId, refundId);

            return respuesta;
        }

        [HttpPost]
        public ActionResult<RefundResponse> PartialRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, double amount)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);
            RefundResponse respuesta = decidir.PartialRefund(paymentId, amount);

            return respuesta;
        }

        [HttpPost]
        public ActionResult<DeleteRefundResponse> DeletePartialRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, long refundId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);
            DeleteRefundResponse respuesta = decidir.DeletePartialRefund(paymentId, refundId);

            return respuesta;
        }

        [HttpPost]
        public ActionResult<PaymentResponse> GetPaymentInfo(int ambienteId, string privateApiKey, string publicApiKey, long paymentId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);
            PaymentResponse respuesta = decidir.GetPaymentInfo(paymentId);

            return respuesta;
        }

        private Payment GetPayment(PaymentDTO payment)
        {
            Payment pago = new Payment();

            pago.amount = payment.amount;
            pago.bin = payment.bin;
            pago.currency = payment.currency;
            pago.description = payment.description;
            pago.installments = payment.installments;
            pago.payment_method_id = payment.payment_method_id;
            pago.payment_type = payment.payment_type;
            pago.establishment_name = payment.establishment_name;
            pago.site_transaction_id = payment.site_transaction_id;
            pago.token = payment.token;
            pago.customer.email = payment.email;
            pago.customer.id = payment.user_id;

            return pago;
        }
    }
}