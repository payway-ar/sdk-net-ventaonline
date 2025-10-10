using Decidir;
using Decidir.Constants;
using Decidir.Model;
using Decidir.Model.CyberSource;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class SubmitPaymentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubmitPaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost]
        public async Task<IActionResult> PerformPayment()
        {
            int ambiente = Ambiente.AMBIENTE_SANDBOX;
            DecidirConnector decidir = null;
            string siteid = null;
            string privatekey = "";
            string publickey = "";
            Payment payment;
            switch (ambiente)
            {
                case Ambiente.AMBIENTE_PRODUCCION:
                    privatekey = "815898d1f3f14e718446f341f5f699e7";
                    publickey = "c035848c2d7d417c8803cfe258a3864c";
                    siteid = "93005849";
                    decidir = new DecidirConnector(ambiente, privatekey, publickey);
                    payment = GetPaymentPRO(siteid);    
                    break;

                default:
                    privatekey = "QqteObli0eb9429AvP2MAPKkUoSz8r3V";
                    publickey = "M5uUE5FilmeiXbaiTAjjY0CMd07zZHXW";
                    siteid = "93007223";
                    decidir = new DecidirConnector(ambiente, privatekey, publickey, "", "", "", "");
                    payment = GetPaymentUAT(siteid);
                    break;

            }

            HealthCheckResponse healthCheckResponse = await decidir.HealthCheckAsync(CancellationToken.None);


            //Para el Ambiente de desarrollo

            string lastfourdigits = Request.Form["last4digits"];
            
            try
            {
                
                var response = await decidir.PaymentAsync(payment, CancellationToken.None);

                return Ok(new { Payment = response });

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        private Payment GetPaymentPRO(string siteid)
        {
            Payment payment = new Payment();



            payment.site_id = siteid;
            payment.site_transaction_id = Guid.NewGuid().ToString();
            payment.payment_method_id = 1;
            payment.token = Request.Form["paymentToken"];
            payment.bin = Request.Form["bin"];
            payment.amount = Convert.ToDouble(Request.Form["amount"]);
            payment.currency = "ARS";
            payment.installments = Convert.ToInt64(Request.Form["installments"]);
            payment.description = "NUTKUT PRUEBA";
            payment.payment_type = "single";
            payment.establishment_name = "NUTPORT**CODFACTURA";
            
            var address = new Address
            {
                email = "llermanos@hotmail.com",
                first_name = "Ariel Alejandro",
                last_name = "Llermanos",
                street1 = "Avenida Juan Bautista Alberdi 2437",
                city = "Capital Federal",
                state = "Capital Federal",
                country = "AR",
                postal_code = "1406",
                phone_number = "541131790000",
                customer_id = "2718088"
            };

            payment.fraud_detection = new RetailFraudDetection
            {
                bill_to = address,
                dispatch_method = "delivery",
                retail_transaction_data = new RetailTransactionData
                {
                    coupon_code = "NUTKUT2024",
                    customer_loyality_number = "2718088",
                    days_to_delivery = "3",
                    items = new List<CSItem>
                    {
                        new CSItem
                        {
                            code = "0001",
                            description = "Producto de prueba",
                            name = "Producto de prueba",
                            quantity = 1,
                            sku = "0001",
                            total_amount = Convert.ToInt64(payment.amount),
                            unit_price = Convert.ToInt64(payment.amount)
                        }
                    },
                    ship_to = address,
                    tax_voucher_required = false
                },
                
                csmdds = new List<Csmdds>
                {
                     new Csmdds{ code=18, description="Compra Online" }
                },
                send_to_cs = false,
                channel = "web",
                customer_in_site = new Customer
                {
                    cellphone_number = "541131790000",
                    date_of_birth = "1979-0604",

                },

                purchase_totals = new PurchaseTotals { amount = Convert.ToInt16(payment.amount), currency = payment.currency },
            };
            return payment;
        }
        private Payment GetPaymentUAT(string siteid)
        {
            Payment payment = new Payment();



            payment.site_id = siteid;
            payment.site_transaction_id = Guid.NewGuid().ToString();
            payment.payment_method_id = 1;
            payment.token = Request.Form["paymentToken"];
            payment.bin = Request.Form["bin"];
            payment.amount = Convert.ToDouble(Request.Form["amount"]);
            payment.currency = "ARS";
            payment.installments = Convert.ToInt64(Request.Form["installments"]);
            payment.description = "NUTKUT PRUEBA";
            payment.payment_type = "single";
            payment.establishment_name = "NUTKUT PRUEBA";

            var address = new Address
            {
                email = "llermanos@hotmail.com",
                first_name = "Ariel Alejandro",
                last_name = "Llermanos",
                street1 = "Avenida Juan Bautista Alberdi 2437",
                city = "Capital Federal",
                state = "Capital Federal",
                country = "AR",
                postal_code = "1406",
                phone_number = "541131790000",
                customer_id = "2718088"
            };

            payment.fraud_detection = new RetailFraudDetection
            {
                bill_to = address,
                dispatch_method = "delivery",
                retail_transaction_data = new RetailTransactionData
                {
                    coupon_code = "NUTKUT2024",
                    customer_loyality_number = "2718088",
                    days_to_delivery = "3",
                    items = new List<CSItem>
                    {
                        new CSItem
                        {
                            code = "0001",
                            description = "Producto de prueba",
                            name = "Producto de prueba",
                            quantity = 1,
                            sku = "0001",
                            total_amount = Convert.ToInt64(payment.amount),
                            unit_price = Convert.ToInt64(payment.amount)
                        }
                    },
                    ship_to = address,
                    tax_voucher_required = false
                },

                csmdds = new List<Csmdds>
                {
                     new Csmdds{ code=18, description="Compra Online" }
                },
                send_to_cs = false,
                channel = "web",
                customer_in_site = new Customer
                {
                    cellphone_number = "541131790000",
                    date_of_birth = "1979-0604",

                },

                purchase_totals = new PurchaseTotals { amount = Convert.ToInt16(payment.amount), currency = payment.currency },
            };
            return payment;
        }
    }
}
