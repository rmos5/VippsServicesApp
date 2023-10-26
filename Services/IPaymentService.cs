namespace VippsServicesApp.Services
{
    public interface IPaymentServicee<TSettings, TPaymentRequest, TPaymentResponse, TCapturePaymentRequest, TCapturePaymentResponse, TCancelPaymentRequest, TCancelPaymentResponse, TRefundRequest, TRefundResponse>
        where TSettings : class
    {
        TPaymentResponse RequestPayment(TPaymentRequest paymentRequest);
        TCapturePaymentResponse CapturePayment(TCapturePaymentRequest capturePaymentRequest);
        TCancelPaymentRequest CancelPayment(TCancelPaymentRequest cancelPaymentRequest);
        TRefundRequest RefundPayment(TRefundRequest refundRequest);
    }
}
