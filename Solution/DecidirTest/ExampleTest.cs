using Decidir;
using Decidir.Constants;
using Decidir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using Decidir.Exceptions;

namespace DecidirTest
{
    [TestClass]
    public class ExampleTest
    {
        [TestMethod]
        public void PaymentExampleTest()
        {
            string privateApiKey = "92b71cf711ca41f78362a7134f87ff65";
            string publicApiKey = "e9cdb99fff374b5f91da4480c8dca741";

            //Para el ambiente de desarrollo
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, privateApiKey, publicApiKey);

            Payment payment = new Payment();

            payment.site_transaction_id = "[ID DE LA TRANSACCIÓN]";
            payment.payment_method_id = 1;
            payment.token = "[TOKEN DE PAGO]";
            payment.bin = "450799";
            payment.amount = 2000;
            payment.currency = "ARS";
            payment.installments = 1;
            payment.description = "";
            payment.payment_type = "single";
            payment.establishment_name = "";

            try
            {
                PaymentResponse resultPaymentResponse = decidir.Payment(payment);
            }
            catch (ResponseException)
            {
            }
        }

        [TestMethod]
        public void GetPaymentInfoExampleTest()
        {
            string privateApiKey = "92b71cf711ca41f78362a7134f87ff65";
            string publicApiKey = "e9cdb99fff374b5f91da4480c8dca741";
            long paymentId = 0;

            //Para el ambiente de desarrollo
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, privateApiKey, publicApiKey);

            PaymentResponse paymentInfoResponse = decidir.GetPaymentInfo(paymentId);
        }

        [TestMethod]
        public void RefundExampleTest()
        {
            string privateApiKey = "92b71cf711ca41f78362a7134f87ff65";
            string publicApiKey = "e9cdb99fff374b5f91da4480c8dca741";
            long paymentId = 0;

            //Para el ambiente de desarrollo
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, privateApiKey, publicApiKey);

            RefundResponse refund = decidir.Refund(paymentId);

        }

        [TestMethod]
        public void DeleteRefundExampleTest()
        {
            string privateApiKey = "92b71cf711ca41f78362a7134f87ff65";
            string publicApiKey = "e9cdb99fff374b5f91da4480c8dca741";
            long paymentId = 0;
            long refundId = 0;

            //Para el ambiente de desarrollo
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, privateApiKey, publicApiKey);

            DeleteRefundResponse deleteRefund = decidir.DeleteRefund(paymentId, refundId);
        }

        [TestMethod]
        public void PartialRefundExampleTest()
        {
            string privateApiKey = "92b71cf711ca41f78362a7134f87ff65";
            string publicApiKey = "e9cdb99fff374b5f91da4480c8dca741";
            long paymentId = 0;
            double refundId = 10.55;

            //Para el ambiente de desarrollo
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, privateApiKey, publicApiKey);

            RefundResponse refund = decidir.PartialRefund(paymentId, refundId);
        }
    }
}
