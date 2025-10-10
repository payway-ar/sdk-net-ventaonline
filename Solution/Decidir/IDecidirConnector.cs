using Decidir.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Decidir
{
    public interface IDecidirConnector
    {
        BatchClosureResponse BatchClosure(string batchClosure);
        Task<BatchClosureResponse> BatchClosureAsync(string batchClosure, CancellationToken cancellationToken);
        CapturePaymentResponse CapturePayment(long paymentId, long amount);
        Task<CapturePaymentResponse> CapturePaymentAsync(long paymentId, long amount, CancellationToken cancellationToken);
        CheckoutResponse CheckoutHash(CheckoutRequest CheckoutRequest);
        Task<CheckoutResponse> CheckoutHashAsync(CheckoutRequest CheckoutRequest, CancellationToken cancellationToken);
        GetCryptogramResponse Cryptogram(CryptogramRequest cryptogramRequest);
        Task<GetCryptogramResponse> CryptogramAsync(CryptogramRequest cryptogramRequest, CancellationToken cancellationToken);
        bool DeleteCardToken(string token);
        Task<bool> DeleteCardTokenAsync(string token, CancellationToken cancellationToken);
        RefundResponse DeletePartialRefund(long paymentId, long? refundId);
        Task<RefundResponse> DeletePartialRefundAsync(long paymentId, long? refundId, CancellationToken cancellationToken);
        RefundResponse DeleteRefund(long paymentId, long? refundId);
        Task<RefundResponse> DeleteRefundAsync(long paymentId, long? refundId, CancellationToken cancellationToken);
        GetAllCardTokensResponse GetAllCardTokens(string userId);
        Task<GetAllCardTokensResponse> GetAllCardTokensAsync(string userId, CancellationToken cancellationToken);
        GetAllPaymentsResponse GetAllPayments(long? offset = null, long? pageSize = null, string siteOperationId = null, string merchantId = null);
        Task<GetAllPaymentsResponse> GetAllPaymentsAsync(long? offset = null, long? pageSize = null, string siteOperationId = null, string merchantId = null, CancellationToken cancellationToken = default);
        GetInternalTokenResponse GetInternalToken(InternalTokenRequest token);
        Task<GetInternalTokenResponse> GetInternalTokenAsync(InternalTokenRequest token, CancellationToken cancellationToken);
        PaymentResponse GetPaymentInfo(long paymentId);
        Task<PaymentResponse> GetPaymentInfoAsync(long paymentId, CancellationToken cancellationToken);
        GetTokenResponse GetToken(TokenRequest token);
        Task<GetTokenResponse> GetTokenAsync(TokenRequest token, CancellationToken cancellationToken);
        GetTokenResponse GetTokenByCardTokenBsa(CardTokenBsa card_token_bsa);
        Task<GetTokenResponse> GetTokenByCardTokenBsaAsync(CardTokenBsa card_token_bsa, CancellationToken cancellationToken);
        HealthCheckResponse HealthCheck();
        Task<HealthCheckResponse> HealthCheckAsync(CancellationToken cancellationToken);
        PaymentResponse InstructionThreeDS(string xConsumerUsername, Instruction3dsData instruction3DsData);
        Task<PaymentResponse> InstructionThreeDSAsync(string xConsumerUsername, Instruction3dsData instruction3DsData, CancellationToken cancellationToken);
        RefundPaymentResponse PartialRefund(long paymentId, RefundAmount amount);
        Task<RefundPaymentResponse> PartialRefundAsync(long paymentId, RefundAmount amount, CancellationToken cancellationToken);
        PaymentResponse Payment(OfflinePayment payment);
        PaymentResponse Payment(Payment payment);
        Task<PaymentResponse> Payment(OfflinePayment payment, CancellationToken cancellationToken);
        Task<PaymentResponse> PaymentAsync(Payment payment, CancellationToken cancellationToken);
        RefundPaymentResponse Refund(long paymentId);
        Task<RefundPaymentResponse> RefundAsync(long paymentId, CancellationToken cancellationToken);
        RefundPaymentResponse RefundSubPayment(long paymentId, RefundSubPaymentRequest refundSubPaymentRequest);
        Task<RefundPaymentResponse> RefundSubPaymentAsync(long paymentId, RefundSubPaymentRequest refundSubPaymentRequest, CancellationToken cancellationToken);
        ValidateResponse Validate(ValidateData validateData);
        Task<ValidateResponse> ValidateAsync(ValidateData validateData, CancellationToken cancellationToken);

    }
}
