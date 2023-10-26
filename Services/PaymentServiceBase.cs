using System;

namespace VippsServicesApp.Services
{
    public abstract class PaymentServiceBase<TSettings, TPaymentRequest, TPaymentResponse, TCapturePaymentRequest, TCapturePaymentResponse, TCancelPaymentRequest, TCancelPaymentResponse, TRefundRequest, TRefundResponse>
        : IPaymentServicee<TSettings, TPaymentRequest, TPaymentResponse, TCapturePaymentRequest, TCapturePaymentResponse, TCancelPaymentRequest, TCancelPaymentResponse, TRefundRequest, TRefundResponse>
        where TSettings : class
    {
        protected TSettings Settings { get; }
        
        protected PaymentServiceBase(TSettings settings) 
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public abstract TPaymentResponse RequestPayment(TPaymentRequest paymentRequest);
        public abstract TCapturePaymentResponse CapturePayment(TCapturePaymentRequest capturePaymentRequest);
        public abstract TCancelPaymentRequest CancelPayment(TCancelPaymentRequest cancelPaymentRequest);
        public abstract TRefundRequest RefundPayment(TRefundRequest refundRequest);
    }
}
