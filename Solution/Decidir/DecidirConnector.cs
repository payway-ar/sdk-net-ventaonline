﻿using System;
using System.Collections.Generic;
using Decidir.Constants;
using Decidir.Model;
using Decidir.Services;
using Newtonsoft.Json;

namespace Decidir
{
    public class DecidirConnector
    {
        #region Constants
        public const string versionDecidir = "1.4.8";

        private const string request_host_sandbox = "https://developers.decidir.com";
        private const string request_host_production = "https://live.decidir.com";
        private const string request_host_qa = "https://qa.decidir.com";
        private const string request_host_desa = "https://dev.decidir.com";
        private const string request_path_payments = "/api/v2/";
        private const string request_path_validate = "/web/";
        private const string request_path_closureQA = "/api/v1/";
        private const string request_path_internal_token = "/api/v1/transaction_gateway/";
        private const string request_path_checkout_production = "/api/checkout/";
        private const string request_path_checkout_sandbox = "/api/checkout/";
        private const string request_path_checkout_qa = "/api/orchestrator/checkout/";

        private const string endPointSandbox = request_host_sandbox + request_path_payments; // https://developers.decidir.com/api/v2/;
        private const string endPointProduction = request_host_production + request_path_payments; //https://live.decidir.com/api/v2/;
        private const string endPointQA = request_host_qa + request_path_payments; //https://qa.decidir.com/api/v2/;
        private const string endPointDesa = request_host_desa + request_path_payments; //https://dev.decidir.com/api/v2/;
        private const string endPointQAClosure = request_host_qa + request_path_closureQA;
        private const string endPointDesaClosure = request_host_desa + request_path_closureQA;

        private const string endPointInternalTokenSandbox = request_host_sandbox + request_path_internal_token;
        private const string endPointInternalTokenProduction = request_host_production + request_path_internal_token;
        private const string endPointInternalTokenQA = request_host_qa + request_path_internal_token;
        private const string endPointInternalTokenDesa = request_host_desa + request_path_internal_token;

        private const string endPointCheckoutSandbox = request_host_sandbox + request_path_checkout_sandbox;
        private const string endPointCheckoutProduction = request_host_production + request_path_checkout_production;
        private const string endPointCheckoutQA = request_host_qa + request_path_checkout_qa;
        private const string endPointCheckoutDesa = request_host_desa + request_path_checkout_qa;

        private const string emptyObject = "{}";

        #endregion

        private string privateApiKey;
        private string publicApiKey;
        private string endpoint;
        private string request_host;

        private string validateApiKey;
        private string merchant;
        private string grouper;
        private string developer;

        private string endPointInternalToken;
        private string endPointCheckout;

        private HealthCheck healthCheckService;
        private Payments paymentService;
        private UserSite userSiteService;
        private CardTokens cardTokensService;
        private BatchClosure bathClosureService;
        private CheckoutService checkoutService;

        private Dictionary<string, string> headers;

        public DecidirConnector(int ambiente, string privateApiKey, string publicApiKey, string validateApiKey = null, string merchant = null, string grouper = "", string developer = "")
        {
            init(ambiente, privateApiKey, publicApiKey, validateApiKey, merchant, grouper, developer);
        }

        public DecidirConnector(string request_host, string request_path, string privateApiKey, string publicApiKey, string validateApiKey = null, string merchant = null, string grouper = "", string developer = "")
        {
            this.request_host = request_host;
            this.endpoint = request_host + request_path;
            init(-1, privateApiKey, publicApiKey, validateApiKey, merchant, grouper, developer);
        }

        private void init(int ambiente, string privateApiKey, string publicApiKey, string validateApiKey, string merchant, string grouper, string developer)
        {
            this.privateApiKey = privateApiKey;
            this.publicApiKey = publicApiKey;
            this.validateApiKey = validateApiKey;
            this.merchant = merchant;
            this.grouper = grouper;
            this.developer = developer;

            this.headers = new Dictionary<string, string>();
            headers.Add("apikey", this.privateApiKey);
            headers.Add("Cache-Control", "no-cache");
            headers.Add("X-Source", getXSource(grouper, developer));

            this.bathClosureService = new BatchClosure(this.endpoint, this.privateApiKey, this.validateApiKey, this.merchant, this.request_host, this.publicApiKey);

            switch (ambiente)
            {
                case Ambiente.AMBIENTE_PRODUCCION:
                    this.endpoint = endPointProduction;
                    this.request_host = request_host_production;
                    this.endPointInternalToken = endPointInternalTokenProduction;
                    this.endPointCheckout = endPointCheckoutProduction;
                    break;
                case Ambiente.AMBIENTE_QA:
                    this.endpoint = endPointQA;
                    this.request_host = request_host_qa;
                    this.endPointInternalToken = endPointInternalTokenQA;
                    this.endPointCheckout = endPointCheckoutQA;
                    this.bathClosureService = new BatchClosure(endPointQAClosure, this.privateApiKey, this.validateApiKey, this.merchant, this.request_host, this.publicApiKey);
                    break;
                case Ambiente.AMBIENTE_SANDBOX:
                    this.endpoint = endPointSandbox;
                    this.request_host = request_host_sandbox;
                    this.endPointInternalToken = endPointInternalTokenSandbox;
                    this.endPointCheckout = endPointCheckoutSandbox;
                    break;
                case Ambiente.AMBIENTE_DESA:
                    this.endpoint = endPointDesa;
                    this.request_host = request_host_desa;
                    this.endPointInternalToken = endPointInternalTokenDesa;
                    this.endPointCheckout = endPointCheckoutDesa;
                    break;
            }
           
            this.healthCheckService = new HealthCheck(this.endpoint, this.headers);
            this.paymentService = new Payments(this.endpoint, this.endPointInternalToken, this.privateApiKey, this.headers, this.validateApiKey, this.merchant, this.request_host, this.publicApiKey);
            this.userSiteService = new UserSite(this.endpoint, this.privateApiKey, this.headers);
            this.cardTokensService = new CardTokens(this.endpoint, this.privateApiKey,this.headers);
            this.checkoutService = new CheckoutService(this.endPointCheckout, this.privateApiKey, this.headers);
        }


        public HealthCheckResponse HealthCheck()
        {
            return this.healthCheckService.Execute();
        }
        
        public PaymentResponse Payment(Payment payment)
        {
            return this.paymentService.ExecutePayment(payment);
        }

        public GetCryptogramResponse Cryptogram(CryptogramRequest cryptogramRequest)
        {
            return this.paymentService.GetCryptogram(cryptogramRequest);
        }

        public PaymentResponse Payment(OfflinePayment payment)
        {
            return this.paymentService.ExecutePayment(payment);
        }

        public CapturePaymentResponse CapturePayment(long paymentId, long amount)
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

        public RefundPaymentResponse Refund(long paymentId)
        {
            return this.paymentService.Refund(paymentId, emptyObject);
        }

        public RefundPaymentResponse RefundSubPayment(long paymentId, RefundSubPaymentRequest refundSubPaymentRequest)
        {
            return this.paymentService.Refund(paymentId, this.ObjectToJson(refundSubPaymentRequest));    
        }

        public BatchClosureResponse BatchClosure(string batchClosure)
        {
            return this.bathClosureService.BatchClosureActive(batchClosure);
        }

        public RefundResponse DeleteRefund(long paymentId, long? refundId)
        {
            return this.paymentService.DeleteRefund(paymentId, refundId);
        }

        public RefundPaymentResponse PartialRefund(long paymentId, RefundAmount amount)
        {
            return this.paymentService.Refund(paymentId, this.ObjectToJson(amount));
        }

        public RefundResponse DeletePartialRefund(long paymentId, long? refundId)
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

        public ValidateResponse Validate(ValidateData validateData)
        {
            return this.paymentService.ValidatePayment(validateData);
        }
        public GetTokenResponse GetTokenByCardTokenBsa(CardTokenBsa card_token_bsa)
        {
            return this.paymentService.GetTokenByCardTokenBsa(card_token_bsa);
        }

        public GetTokenResponse GetToken(TokenRequest token)
        {
            return this.paymentService.GetToken(token);
        }

        public GetInternalTokenResponse GetInternalToken(InternalTokenRequest token)
        {
            return this.paymentService.GetInternalToken(token);
        }

        public PaymentResponse InstructionThreeDS(string xConsumerUsername, Instruction3dsData instruction3DsData)
        {
            return this.paymentService.InstructionThreeDS(xConsumerUsername, instruction3DsData);
        }

        public CheckoutResponse CheckoutHash(CheckoutRequest CheckoutRequest)
        {
            return this.checkoutService.CheckoutHash(CheckoutRequest);
        }

        private string getXSource(String grouper, String developer)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("service", "SDK-NET");
            header.Add("grouper", grouper);
            header.Add("developer", developer);

            String headerJson = JsonConvert.SerializeObject(header, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            byte[] headerJsonBytes = System.Text.Encoding.UTF8.GetBytes(headerJson);

            return System.Convert.ToBase64String(headerJsonBytes);
        }

        private String ObjectToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);
        }

    }
}
