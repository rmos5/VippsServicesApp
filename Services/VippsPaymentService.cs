using System;
using System.Collections.Generic;
using System.Net.Http;
using VippsApi;

namespace VippsServicesApp.Services
{
    public interface IVippsPaymentService : IPaymentService<CreatePaymentRequest, CreatePaymentResponse>
    {
    }

    public class VippsServiceSettings : Dictionary<string, string>
    {
        public const string BaseAddress = "BaseUrl";
        public const string Timeout = "Timeout";
        public const string ocp_Apim_Subscription_Key = "ocp_Apim_Subscription_Key";
        public const string merchant_Serial_Number = "merchant_Serial_Number";
        public const string vipps_System_Name = "vipps_System_Name";
        public const string vipps_System_Version = "vipps_System_Version";
        public const string vipps_System_Plugin_Name = "vipps_System_Plugin_Name";
        public const string vipps_System_Plugin_Version = "vipps_System_Plugin_Version";
    }

    internal class VippsPaymentService : IVippsPaymentService
    {
        protected VippsServiceSettings Settings { get; }

        public VippsPaymentService(VippsServiceSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public CreatePaymentResponse DoPayment(CreatePaymentRequest paymentRequest)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Settings[VippsServiceSettings.BaseAddress], UriKind.Absolute),
                Timeout = TimeSpan.Parse(Settings[VippsServiceSettings.Timeout])
            };

            string idempotency_Key = Guid.NewGuid().ToString();
            string ocp_Apim_Subscription_Key = Settings[VippsServiceSettings.ocp_Apim_Subscription_Key];
            string merchant_Serial_Number = Settings[VippsServiceSettings.merchant_Serial_Number]; ;
            string vipps_System_Name = Settings[VippsServiceSettings.vipps_System_Name]; ;
            string vipps_System_Version = Settings[VippsServiceSettings.vipps_System_Version]; ;
            string vipps_System_Plugin_Name = Settings[VippsServiceSettings.vipps_System_Plugin_Name]; ;
            string vipps_System_Plugin_Version = Settings[VippsServiceSettings.vipps_System_Plugin_Version]; ;

            EPayment service = new EPayment(client);
            CreatePaymentResponse result = service.CreatePaymentAsync
                (paymentRequest,
                idempotency_Key,
                ocp_Apim_Subscription_Key,
                merchant_Serial_Number,
                vipps_System_Name,
                vipps_System_Version,
                vipps_System_Plugin_Name,
                vipps_System_Plugin_Version).Result;

            return result;
        }
    }
}
