using Decidir;
using Decidir.Model;
using DecidirExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            DecidirConnector decidir = new DecidirConnector(payment.AmbienteId, payment.privateApiKey, payment.publicApiKey);

            PaymentResponse respuesta = decidir.Payment(GetPayment(payment));

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult Refund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);

            RefundResponse respuesta = decidir.Refund(paymentId);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult DeleteRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, long refundId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);

            DeleteRefundResponse respuesta = decidir.DeleteRefund(paymentId, refundId);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult PartialRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, double amount)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);

            RefundResponse respuesta = decidir.PartialRefund(paymentId, amount);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult DeletePartialRefund(int ambienteId, string privateApiKey, string publicApiKey, long paymentId, long refundId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);

            DeleteRefundResponse respuesta = decidir.DeletePartialRefund(paymentId, refundId);

            return Json(respuesta);
        }

        [HttpPost]
        public ActionResult GetPaymentInfo(int ambienteId, string privateApiKey, string publicApiKey, long paymentId)
        {
            DecidirConnector decidir = new DecidirConnector(ambienteId, privateApiKey, publicApiKey);

            PaymentResponse respuesta = decidir.GetPaymentInfo(paymentId);

            return Json(respuesta);
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