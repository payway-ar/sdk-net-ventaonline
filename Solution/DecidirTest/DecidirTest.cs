using Decidir;
using Decidir.Constants;
using Decidir.Model;
using Decidir.Model.CyberSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using Decidir.Exceptions;
using DecidirTest.Model;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DecidirTest
{
    [TestClass]
    public class DecidirTest
    {
        [TestMethod]
        public void PaymentTest()
        {
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
            CardData data = GetCardData();
         //   CardTokenResponse resultCardData = new CardTokenResponse();
            PaymentResponse resultPaymentResponse = new PaymentResponse();
            GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();

            try
            {
              //  resultCardData = decidir.GetToken(data);

                Payment payment = GetPayment(1);

                resultPaymentResponse = decidir.Payment(payment);

                Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                pagos = decidir.GetAllPayments(null, null, null, "00030118");

                Assert.AreEqual(true, pagos.results.Count >= 0);
            }
            catch (ResponseException)
            {
               
                Assert.AreEqual(true, false);
            }
        }

        private Payment GetPayment(int v)
        {
            throw new NotImplementedException();
        }

        /*  [TestMethod]
          public void GetPaymentInfoTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardData data = GetCardData();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              PaymentResponse paymentInfoResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  Thread.Sleep(4000);

                  paymentInfoResponse = decidir.GetPaymentInfo(resultPaymentResponse.id);

                  Assert.AreEqual(true, pagos.results.Count >= 0);
              }
              catch (ResponseException)
              {
                  Assert.AreEqual(true, false);
              }
          }

          [TestMethod]
          public void PaymentCSRetailTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardDataCyberSource data = GetCardDataCyberSource();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);
                  payment.fraud_detection = GetRetailFraudDetection();

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  pagos = decidir.GetAllPayments(null, null, null, "00030118");

                  Assert.AreEqual(true, pagos.results.Count >= 0);
              }
              catch (ResponseException ex)
              {
                  Assert.AreEqual(true, false);
              }
          }

          [TestMethod]
          public void PaymentCSDigitalGoodsTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardDataCyberSource data = GetCardDataCyberSource();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);
                  payment.fraud_detection = GetDigitalGoodsFraudDetection();

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  pagos = decidir.GetAllPayments(null, null, null, "00030118");

                  Assert.AreEqual(true, pagos.results.Count >= 0);
              }
              catch (Exception)
              {
                  Assert.AreEqual(true, false);
              }
          }

          [TestMethod]
          public void PaymentCSTicketingTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardDataCyberSource data = GetCardDataCyberSource();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);
                  payment.fraud_detection = GetTicketingFraudDetection();

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  pagos = decidir.GetAllPayments(null, null, null, "00030118");

                  Assert.AreEqual(true, pagos.results.Count >= 0);
              }
              catch (Exception ex)
              {
                  Assert.AreEqual(true, false);
              }
          }

          [TestMethod]
          public void RefundTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardData data = GetCardData();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();
              RefundResponse refund = new RefundResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  //Wait for Refund
                  Thread.Sleep(5000);
                  refund = decidir.Refund(resultPaymentResponse.id);

                  Assert.AreEqual(payment.amount * 100, refund.amount);
                  Assert.AreEqual("approved", refund.status);
              }
              catch (Exception)
              {
                  Assert.AreEqual(true, false);
              }
          }

          [TestMethod]
          public void AnulacionRefundTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardData data = GetCardData();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();
              RefundResponse refund = new RefundResponse();
              DeleteRefundResponse deleteRefund = new DeleteRefundResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  //Wait for Refund
                  Thread.Sleep(5000);
                  refund = decidir.Refund(resultPaymentResponse.id);

                  Assert.AreEqual(payment.amount * 100, refund.amount);
                  Assert.AreEqual("approved", refund.status);

                  deleteRefund = decidir.DeleteRefund(resultPaymentResponse.id, refund.id);
              }
              catch (Exception)
              {
                  Assert.AreEqual(true, false);
              }
          }

          [TestMethod]
          public void PartialRefundTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardData data = GetCardData();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();
              RefundResponse refund = new RefundResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  //Wait for Refund
                  Thread.Sleep(5000);
                  refund = decidir.PartialRefund(resultPaymentResponse.id, 1000);

                  Assert.AreEqual(1000 * 100, refund.amount);
                  Assert.AreEqual("approved", refund.status);
              }
              catch (Exception)
              {
                  Assert.AreEqual(true, false);
              }
          }

          [TestMethod]
          public void AnulacionPartialRefundTest()
          {
              DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
              CardData data = GetCardData();
              CardTokenResponse resultCardData = new CardTokenResponse();
              PaymentResponse resultPaymentResponse = new PaymentResponse();
              GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();
              RefundResponse refund = new RefundResponse();
              DeleteRefundResponse deleteRefund = new DeleteRefundResponse();

              try
              {
                  resultCardData = decidir.GetToken(data);

                  Payment payment = GetPayment(resultCardData.id);

                  resultPaymentResponse = decidir.Payment(payment);

                  Assert.AreEqual(resultPaymentResponse.bin, payment.bin);
                  Assert.AreEqual(resultPaymentResponse.amount, payment.amount * 100);
                  Assert.AreEqual(resultPaymentResponse.site_transaction_id, payment.site_transaction_id);

                  //Wait for Refund
                  Thread.Sleep(5000);
                  refund = decidir.PartialRefund(resultPaymentResponse.id, 1000);

                  Assert.AreEqual(1000 * 100, refund.amount);
                  Assert.AreEqual("approved", refund.status);

                  deleteRefund = decidir.DeletePartialRefund(resultPaymentResponse.id, refund.id);
              }
              catch (Exception)
              {
                  Assert.AreEqual(true, false);
              }
          }
          */
          private CardData GetCardData()
          {
              CardData data = new CardData();

              data.card_number = "4507990000004905";
              data.card_expiration_month = "08";
              data.card_expiration_year = "20";
              data.security_code = "123";
              data.card_holder_name = "John Doe";
              data.card_holder_identification.type = "dni";
              data.card_holder_identification.number = "25123456";

              return data;
          }
          

          private Payment GetPayment(string resultCardDataId)
          {
              Payment payment = new Payment();

              payment.site_transaction_id = GetTimestamp(DateTime.Now);
              payment.payment_method_id = 1;
              payment.token = resultCardDataId;
              payment.bin = "450799";
              payment.amount = 2000;
              payment.currency = "ARS";
              payment.installments = 1;
              payment.description = "";
              payment.payment_type = "single";

              return payment;
          }

          private RetailFraudDetection GetRetailFraudDetection()
          {
              RetailFraudDetection retail = new RetailFraudDetection();

              retail.channel = "Web/Mobile/Telefonica";

              //bill_to
              retail.bill_to.city = "Buenos Aires";
              retail.bill_to.country = "AR";
              retail.bill_to.customer_id = "useridprueba";
              retail.bill_to.email = "accept@decidir.com.ar";
              retail.bill_to.first_name = "nombre";
              retail.bill_to.last_name = "apellido";
              retail.bill_to.phone_number = "1512341234";
              retail.bill_to.postal_code = "1427";
              retail.bill_to.state = "BA";
              retail.bill_to.street1 = "Cerrito 123";
              retail.bill_to.street2 = "Mexico 123";

              //purchase_totals
              retail.purchase_totals.currency = "ARS";
              retail.purchase_totals.amount = 2000 * 100;

              //customer_in_site
              retail.customer_in_site.days_in_site = 243;
              retail.customer_in_site.is_guest = false;
              retail.customer_in_site.password = "abracadabra";
              retail.customer_in_site.num_of_transactions = 1;
              retail.customer_in_site.cellphone_number = "12121";
              retail.customer_in_site.date_of_birth = "129412";
              retail.customer_in_site.street = "RIO 4041";

              //retail_transaction_data
              retail.retail_transaction_data.ship_to.city = "Buenos Aires";
              retail.retail_transaction_data.ship_to.country = "AR";
              retail.retail_transaction_data.ship_to.customer_id = "useridprueba";
              retail.retail_transaction_data.ship_to.email = "accept@decidir.com.ar";
              retail.retail_transaction_data.ship_to.first_name = "nombre";
              retail.retail_transaction_data.ship_to.last_name = "apellido";
              retail.retail_transaction_data.ship_to.phone_number = "1512341234";
              retail.retail_transaction_data.ship_to.postal_code = "1427";
              retail.retail_transaction_data.ship_to.state = "BA";
              retail.retail_transaction_data.ship_to.street1 = "Cerrito 123";
              retail.retail_transaction_data.ship_to.street2 = "Mexico 123";

              retail.retail_transaction_data.days_to_delivery = "55";
           //   retail.retail_transaction_data.dispatch_method = "storepickup";
              retail.retail_transaction_data.tax_voucher_required = true;
              retail.retail_transaction_data.customer_loyality_number = "123232";
              retail.retail_transaction_data.coupon_code = "cupon22";

              CSItem item = new CSItem();
              item.code = "estoesunapruebadecs";
              item.description = "Prueba de CyberSource";
              item.name = "CyberSource";
              item.sku = "prueba";
              item.total_amount = 2000 * 100;
              item.quantity = 1;
              item.unit_price = 2000 * 100;
              retail.retail_transaction_data.items.Add(item);

              for (int i = 17; i < 35; i++)
              {
                  Csmdds csmdds = new Csmdds();

                  csmdds.code = i;
                  csmdds.description = "MDD" + i.ToString();

                  retail.csmdds.Add(csmdds);
              }

              for (int i = 43; i < 101; i++)
              {
                  Csmdds csmdds = new Csmdds();

                  csmdds.code = i;
                  csmdds.description = "MDD" + i.ToString();

                  retail.csmdds.Add(csmdds);
              }

              return retail;
          }

          private DigitalGoodsFraudDetection GetDigitalGoodsFraudDetection()
          {
              DigitalGoodsFraudDetection digitalGoods = new DigitalGoodsFraudDetection();

              digitalGoods.channel = "Web/Mobile/Telefonica";

              //bill_to
              digitalGoods.bill_to.city = "Buenos Aires";
              digitalGoods.bill_to.country = "AR";
              digitalGoods.bill_to.customer_id = "useridprueba";
              digitalGoods.bill_to.email = "accept@decidir.com.ar";
              digitalGoods.bill_to.first_name = "nombre";
              digitalGoods.bill_to.last_name = "apellido";
              digitalGoods.bill_to.phone_number = "1512341234";
              digitalGoods.bill_to.postal_code = "1427";
              digitalGoods.bill_to.state = "BA";
              digitalGoods.bill_to.street1 = "Cerrito 123";
              digitalGoods.bill_to.street2 = "Mexico 123";

              //purchase_totals
              digitalGoods.purchase_totals.currency = "ARS";
              digitalGoods.purchase_totals.amount = 2000 * 100;

              //customer_in_site
              digitalGoods.customer_in_site.days_in_site = 243;
              digitalGoods.customer_in_site.is_guest = false;
              digitalGoods.customer_in_site.password = "abracadabra";
              digitalGoods.customer_in_site.num_of_transactions = 1;
              digitalGoods.customer_in_site.cellphone_number = "12121";
              digitalGoods.customer_in_site.date_of_birth = "129412";
              digitalGoods.customer_in_site.street = "RIO 4041";

              //device_unique_id
              digitalGoods.device_unique_id = "devicefingerprintid";

              //digital_goods_transaction_data
              digitalGoods.digital_goods_transaction_data.delivery_type = "Pick up";

              CSItem item = new CSItem();
              item.code = "estoesunapruebadecs";
              item.description = "Prueba de CyberSource";
              item.name = "CyberSource";
              item.sku = "prueba";
              item.total_amount = 2000 * 100;
              item.quantity = 1;
              item.unit_price = 2000 * 100;
              digitalGoods.digital_goods_transaction_data.items.Add(item);

              for (int i = 17; i < 35; i++)
              {
                  Csmdds csmdds = new Csmdds();

                  csmdds.code = i;
                  csmdds.description = "MDD" + i.ToString();

                  digitalGoods.csmdds.Add(csmdds);
              }

              for (int i = 43; i < 101; i++)
              {
                  Csmdds csmdds = new Csmdds();

                  csmdds.code = i;
                  csmdds.description = "MDD" + i.ToString();

                  digitalGoods.csmdds.Add(csmdds);
              }

              return digitalGoods;
          }

          private TicketingFraudDetection GetTicketingFraudDetection()
          {
              TicketingFraudDetection ticketing = new TicketingFraudDetection();

              ticketing.channel = "Web/Mobile/Telefonica";

              //bill_to
              ticketing.bill_to.city = "Buenos Aires";
              ticketing.bill_to.country = "AR";
              ticketing.bill_to.customer_id = "useridprueba";
              ticketing.bill_to.email = "accept@decidir.com.ar";
              ticketing.bill_to.first_name = "nombre";
              ticketing.bill_to.last_name = "apellido";
              ticketing.bill_to.phone_number = "1512341234";
              ticketing.bill_to.postal_code = "1427";
              ticketing.bill_to.state = "BA";
              ticketing.bill_to.street1 = "Cerrito 123";
              ticketing.bill_to.street2 = "Mexico 123";

              //purchase_totals
              ticketing.purchase_totals.currency = "ARS";
              ticketing.purchase_totals.amount = 2000 * 100;

              //customer_in_site
              ticketing.customer_in_site.days_in_site = 243;
              ticketing.customer_in_site.is_guest = false;
              ticketing.customer_in_site.password = "abracadabra";
              ticketing.customer_in_site.num_of_transactions = 1;
              ticketing.customer_in_site.cellphone_number = "12121";
              ticketing.customer_in_site.date_of_birth = "129412";
              ticketing.customer_in_site.street = "RIO 4041";

              //ticketing_transaction_data
              ticketing.ticketing_transaction_data.days_to_event = 55;
              ticketing.ticketing_transaction_data.delivery_type = "Pick up";

              CSItem item = new CSItem();
              item.code = "estoesunapruebadecs";
              item.description = "Prueba de CyberSource";
              item.name = "CyberSource";
              item.sku = "prueba";
              item.total_amount = 2000 * 100;
              item.quantity = 1;
              item.unit_price = 2000 * 100;
              ticketing.ticketing_transaction_data.items.Add(item);

              ticketing.csmdds.Add(new Csmdds() { code = 12, description = "MDD12" });

              for (int i = 14; i < 33; i++)
              {
                  Csmdds csmdds = new Csmdds();

                  csmdds.code = i;
                  csmdds.description = "MDD" + i.ToString();

                  ticketing.csmdds.Add(csmdds);
              }

              for (int i = 43; i < 101; i++)
              {
                  Csmdds csmdds = new Csmdds();

                  csmdds.code = i;
                  csmdds.description = "MDD" + i.ToString();

                  ticketing.csmdds.Add(csmdds);
              }

              return ticketing;
          }

          private string GetTimestamp(DateTime value)
          {
              return value.ToString("yyyyMMddHHmmss");
          } 

         


    }
}
