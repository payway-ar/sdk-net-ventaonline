using Decidir;
using Decidir.Model;
using DecidirExample.Models;
using System.Web.Mvc;

namespace DecidirExample.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Payment(PaymentDTO payment)
        {
            DecidirConnector decidir = GetDecidirConnector(payment.AmbienteId, payment.privateApiKey, payment.publicApiKey, payment.request_host, payment.request_path);

            PaymentResponse respuesta = decidir.Payment(GetPayment(payment));

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult Refund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, string request_host, string request_path)
        {
            DecidirConnector decidir = GetDecidirConnector(ambienteId, privateApiKey, publicApiKey, request_host, request_path);

            RefundResponse respuesta = decidir.Refund(paymentId);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult DeleteRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, long refundId, string request_host, string request_path)
        {
            DecidirConnector decidir = GetDecidirConnector(ambienteId, privateApiKey, publicApiKey, request_host, request_path);

            DeleteRefundResponse respuesta = decidir.DeleteRefund(paymentId, refundId);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult PartialRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, double amount, string request_host, string request_path)
        {
            DecidirConnector decidir = GetDecidirConnector(ambienteId, privateApiKey, publicApiKey, request_host, request_path);

            RefundResponse respuesta = decidir.PartialRefund(paymentId, amount);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult DeletePartialRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, long refundId, string request_host, string request_path)
        {
            DecidirConnector decidir = GetDecidirConnector(ambienteId, privateApiKey, publicApiKey, request_host, request_path);

            DeleteRefundResponse respuesta = decidir.DeletePartialRefund(paymentId, refundId);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult GetPaymentInfo(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, string request_host, string request_path)
        {
            DecidirConnector decidir = GetDecidirConnector(ambienteId, privateApiKey, publicApiKey, request_host, request_path);

            PaymentResponse respuesta = decidir.GetPaymentInfo(paymentId);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult GetTokenBSA(CardTokenBsaDTO cardTokenBsaDTO)
        {
            DecidirConnector decidir = GetDecidirConnector(cardTokenBsaDTO.AmbienteId, cardTokenBsaDTO.privateApiKey, cardTokenBsaDTO.publicApiKey, cardTokenBsaDTO.request_host, cardTokenBsaDTO.request_path);

            GetTokenResponse respuesta = decidir.GetToken(cardTokenBsaDTO.cardTokenBsa);

            return Json(respuesta);
        }

        private DecidirConnector GetDecidirConnector(int ambienteId, string privateApiKey, string publicApiKey, string request_host, string request_path)
        {
            if (request_host != null && request_path != null)
            {
                return new DecidirConnector(request_host, request_path, privateApiKey, publicApiKey);
            }
            else
            {
                return new DecidirConnector(ambienteId, privateApiKey, publicApiKey);
            }
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