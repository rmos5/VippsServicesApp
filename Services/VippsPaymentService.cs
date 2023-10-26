using System;
using System.Collections.Generic;
using System.Net.Http;
using VippsApi;

namespace VippsServicesApp.Services
{
    public class VippsServiceSettings : Dictionary<string, string>
    {
        public const string BaseAddress = "BaseAddress";
        public const string PaymentUri = "PaymentUri";
        public const string Timeout = "Timeout";
        public const string ocp_Apim_Subscription_Key = "ocp_Apim_Subscription_Key";
        public const string merchant_Serial_Number = "merchant_Serial_Number";
        public const string vipps_System_Name = "vipps_System_Name";
        public const string vipps_System_Version = "vipps_System_Version";
        public const string vipps_System_Plugin_Name = "vipps_System_Plugin_Name";
        public const string vipps_System_Plugin_Version = "vipps_System_Plugin_Version";

        public VippsServiceSettings()
        {
            Add(BaseAddress, "https://apitest.vipps.no");
            Add(PaymentUri, "/epayment/v1/payments");
            Add(Timeout, TimeSpan.FromSeconds(5).ToString());
            Add(ocp_Apim_Subscription_Key, "");
            Add(merchant_Serial_Number, "");
            Add(vipps_System_Name, "");
            Add(vipps_System_Version, "");
            Add(vipps_System_Plugin_Name, "");
            Add(vipps_System_Plugin_Version, "");
        }
    }

    internal class VippsPaymentService : PaymentServiceBase<VippsServiceSettings, CreatePaymentRequest, CreatePaymentResponse, CaptureModificationRequest, ModificationResponse, string, ModificationResponse, RefundModificationRequest, ModificationResponse>
    {
        public VippsPaymentService(VippsServiceSettings settings) : base(settings)
        {
        }

        public override CreatePaymentResponse RequestPayment(CreatePaymentRequest paymentRequest)
        {
            Uri serviceUri = new Uri(new Uri(Settings[VippsServiceSettings.BaseAddress]), Settings[VippsServiceSettings.PaymentUri]);
            HttpClient client = new HttpClient
            {
                BaseAddress = serviceUri,
                Timeout = TimeSpan.Parse(Settings[VippsServiceSettings.Timeout]),
            };

            EPayment service = new EPayment(client);
            string idempotency_Key = Guid.NewGuid().ToString();
            CreatePaymentResponse result = service.CreatePaymentAsync
                (paymentRequest,
                idempotency_Key,
                Settings[VippsServiceSettings.ocp_Apim_Subscription_Key],
                Settings[VippsServiceSettings.merchant_Serial_Number],
                Settings[VippsServiceSettings.vipps_System_Name],
                Settings[VippsServiceSettings.vipps_System_Version],
                Settings[VippsServiceSettings.vipps_System_Plugin_Name],
                Settings[VippsServiceSettings.vipps_System_Plugin_Version]).Result;

            return result;
        }

        public override string CancelPayment(string cancelPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public override ModificationResponse CapturePayment(CaptureModificationRequest capturePaymentRequest)
        {
            throw new NotImplementedException();
        }

        public override RefundModificationRequest RefundPayment(RefundModificationRequest refundRequest)
        {
            throw new NotImplementedException();
        }
    }
}
