using Decidir.Constants;
using Decidir.Model;
using Decidir.Services;

namespace Decidir
{
    public class DecidirConnector
    {
        #region Constants
        public const string versionDecidir = "1.1.1";
        
        private const string endPointSandbox = "https://developers.decidir.com/api/v2/";

        private const string endPointProduction = "https://live.decidir.com/api/v2/";
        #endregion

        private string privateApiKey;
        private string publicApiKey;
        private int ambiente;
        private string endpoint;

        private HealthCheck healthCheckService;
        private Payments paymentService;
        private UserSite userSiteService;
        private CardTokens cardTokensService;

        public DecidirConnector(int ambiente, string privateApiKey, string publicApiKey)
        {
            this.ambiente = ambiente;
            this.privateApiKey = privateApiKey;
            this.publicApiKey = publicApiKey;

            if (ambiente == Ambiente.AMBIENTE_PRODUCCION)
            {
                this.endpoint = endPointProduction;
            }
            else
            {
                this.endpoint = endPointSandbox;
            }

            this.healthCheckService = new HealthCheck(this.endpoint);
            this.paymentService = new Payments(this.endpoint, this.privateApiKey);
            this.userSiteService = new UserSite(this.endpoint, this.privateApiKey);
            this.cardTokensService = new CardTokens(this.endpoint, this.privateApiKey);
        }

        public HealthCheckResponse HealthCheck()
        {
            return this.healthCheckService.Execute();
        }
        
        public PaymentResponse Payment(Payment payment)
        {
            return this.paymentService.ExecutePayment(payment);
        }

        public CapturePaymentResponse CapturePayment(long paymentId, double amount)
        {
            return this.paymentService.CapturePayment(paymentId, amount);
        }

        public GetAllPaymentsResponse GetAllPayments(long? offset = null, long? pageSize = null, string siteOperationId = null, string merchantId = null)
        {
            return this.paymentService.GetAllPayments(offset, pageSize, siteOperationId, merchantId);
        }

        public PaymentResponse GetPaymentInfo(long paymentId)
        {
            return this.paymentService.GetPaymentInfo(paymentId);
        }

        public RefundResponse Refund(long paymentId)
        {
            return this.paymentService.Refund(paymentId);
        }

        public DeleteRefundResponse DeleteRefund(long paymentId, long refundId)
        {
            return this.paymentService.DeleteRefund(paymentId, refundId);
        }

        public RefundResponse PartialRefund(long paymentId, double amount)
        {
            return this.paymentService.PartialRefund(paymentId, amount);
        }

        public DeleteRefundResponse DeletePartialRefund(long paymentId, long refundId)
        {
            return this.paymentService.DeletePartialRefund(paymentId, refundId);
        }

        public GetAllCardTokensResponse GetAllCardTokens(string userId)
        {
            return this.userSiteService.GetAllTokens(userId);
        }

        public bool DeleteCardToken(string token)
        {
            return this.cardTokensService.DeleteCardToken(token);
        }
    }
}
