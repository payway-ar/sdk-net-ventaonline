using Microsoft.VisualStudio.TestTools.UnitTesting;
using Decidir;
using Decidir.Model;
using Decidir.Constants;
using System;
using Decidir.Model.CyberSource;

namespace DecidirTest.Services
{
    [TestClass]
    public class PaymentsTest
    {
        [TestMethod]
        public void GetAllPaymentsTest()
        {
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
            GetAllPaymentsResponse pagos = new GetAllPaymentsResponse();

            try
            {
                pagos = decidir.GetAllPayments(null, null, null, "00030118");
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }

            Assert.AreEqual(true, pagos.results.Count >= 0);
        }

        [TestMethod]
        public void PaymentCSRetailTest()
        {
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
            Payment data = new Payment();
            string paymentJson;

            data.site_transaction_id = GetTimestamp(DateTime.Now);
            data.payment_method_id = 1;
            data.token = "d52b7701-87bb-4926-b4a6-94267a5a78b4";
            data.bin = "450799";
            data.amount = 2000;
            data.currency = "ARS";
            data.installments = 1;
            data.description = "";
            data.payment_type = "single";

            data.fraud_detection = new RetailFraudDetection();

            try
            {
                paymentJson = Payment.toJson(data);
                Assert.AreEqual("{\"site_transaction_id\":\"" + data.site_transaction_id + "\",\"token\":\"d52b7701-87bb-4926-b4a6-94267a5a78b4\",\"user_id\":null,\"payment_method_id\":1,\"bin\":\"450799\",\"amount\":2000.0,\"currency\":\"ARS\",\"installments\":1,\"description\":\"\",\"payment_type\":\"single\",\"sub_payments\":[],\"fraud_detection\":{\"retail_transaction_data\":{\"ship_to\":{\"city\":null,\"country\":null,\"customer_id\":null,\"email\":null,\"first_name\":null,\"last_name\":null,\"phone_number\":null,\"postal_code\":null,\"state\":null,\"street1\":null,\"street2\":null},\"days_to_delivery\":null,\"dispatch_method\":null,\"tax_voucher_required\":false,\"customer_loyality_number\":null,\"coupon_code\":null,\"items\":[]},\"send_to_cs\":true,\"channel\":null,\"bill_to\":{\"city\":null,\"country\":null,\"customer_id\":null,\"email\":null,\"first_name\":null,\"last_name\":null,\"phone_number\":null,\"postal_code\":null,\"state\":null,\"street1\":null,\"street2\":null},\"purchase_totals\":{\"currency\":null,\"amount\":0},\"customer_in_site\":{\"days_in_site\":0,\"is_guest\":false,\"password\":null,\"num_of_transactions\":0,\"cellphone_number\":null,\"date_of_birth\":null,\"street\":null},\"csmdds\":[]}}", paymentJson);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        [TestMethod]
        public void PaymentCSDigitalGoodsTest()
        {
            DecidirConnector decidir = new DecidirConnector(Ambiente.AMBIENTE_SANDBOX, "566f2c897b5e4bfaa0ec2452f5d67f13", "b192e4cb99564b84bf5db5550112adea");
            Payment data = new Payment();
            string paymentJson;

            data.site_transaction_id = GetTimestamp(DateTime.Now);
            data.payment_method_id = 1;
            data.token = "d52b7701-87bb-4926-b4a6-94267a5a78b4";
            data.bin = "450799";
            data.amount = 2000;
            data.currency = "ARS";
            data.installments = 1;
            data.description = "";
            data.payment_type = "single";

            data.fraud_detection = new DigitalGoodsFraudDetection();

            try
            {
                paymentJson = Payment.toJson(data);
                Assert.AreEqual("{\"site_transaction_id\":\"" + data.site_transaction_id + "\",\"token\":\"d52b7701-87bb-4926-b4a6-94267a5a78b4\",\"user_id\":null,\"payment_method_id\":1,\"bin\":\"450799\",\"amount\":2000.0,\"currency\":\"ARS\",\"installments\":1,\"description\":\"\",\"payment_type\":\"single\",\"sub_payments\":[],\"fraud_detection\":{\"digital_goods_transaction_data\":{\"delivery_type\":null,\"items\":[]},\"device_unique_id\":null,\"send_to_cs\":true,\"channel\":null,\"bill_to\":{\"city\":null,\"country\":null,\"customer_id\":null,\"email\":null,\"first_name\":null,\"last_name\":null,\"phone_number\":null,\"postal_code\":null,\"state\":null,\"street1\":null,\"street2\":null},\"purchase_totals\":{\"currency\":null,\"amount\":0},\"customer_in_site\":{\"days_in_site\":0,\"is_guest\":false,\"password\":null,\"num_of_transactions\":0,\"cellphone_number\":null,\"date_of_birth\":null,\"street\":null},\"csmdds\":[]}}", paymentJson);
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
            Payment data = new Payment();
            string paymentJson;

            data.site_transaction_id = GetTimestamp(DateTime.Now);
            data.payment_method_id = 1;
            data.token = "d52b7701-87bb-4926-b4a6-94267a5a78b4";
            data.bin = "450799";
            data.amount = 2000;
            data.currency = "ARS";
            data.installments = 1;
            data.description = "";
            data.payment_type = "single";
            data.establishment_name = "";    

            data.fraud_detection = new TicketingFraudDetection();

            try
            {
                paymentJson = Payment.toJson(data);
                Assert.AreEqual("{\"site_transaction_id\":\"" + data.site_transaction_id + "\",\"token\":\"d52b7701-87bb-4926-b4a6-94267a5a78b4\",\"user_id\":null,\"payment_method_id\":1,\"bin\":\"450799\",\"amount\":2000.0,\"currency\":\"ARS\",\"installments\":1,\"description\":\"\",\"payment_type\":\"single\", \"establishment_name\":\"\",\"sub_payments\":[],\"fraud_detection\":{\"ticketing_transaction_data\":{\"days_to_event\":null,\"delivery_type\":null,\"items\":[]},\"send_to_cs\":true,\"channel\":null,\"bill_to\":{\"city\":null,\"country\":null,\"customer_id\":null,\"email\":null,\"first_name\":null,\"last_name\":null,\"phone_number\":null,\"postal_code\":null,\"state\":null,\"street1\":null,\"street2\":null},\"purchase_totals\":{\"currency\":null,\"amount\":0},\"customer_in_site\":{\"days_in_site\":0,\"is_guest\":false,\"password\":null,\"num_of_transactions\":0,\"cellphone_number\":null,\"date_of_birth\":null,\"street\":null},\"csmdds\":[]}}", paymentJson);
            }
            catch (Exception)
            {
                Assert.AreEqual(true, false);
            }
        }

        private string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmss");
        }
    }
}
