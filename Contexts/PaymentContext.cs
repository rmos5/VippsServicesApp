using Context;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using VippsApi;

namespace VippsServicesApp.Contexts
{
    public partial class PaymentContext : ContextBase
    {
        private sealed class PaymentCommandImpl : CommandBase<PaymentContext>
        {
            public PaymentCommandImpl(PaymentContext context) : base(context)
            {
            }

            public override bool CanExecute(object parameter)
            {
                return Context.CanExecutePaymentCommand(parameter);
            }

            public async override void Execute(object parameter)
            {
                await Context.ExecutePaymentCommand(parameter);
            }
        }

        public CommandBase PaymentCommand { get; }

        public PaymentContext()
        {
            PaymentCommand = new PaymentCommandImpl(this);
        }
        
        protected override string SetTitle()
        {
            return "Payment";
        }


        private bool CanExecutePaymentCommand(object parameter)
        {
            return true;
        }

        private async Task ExecutePaymentCommand(object parameter)
        {
            string description = "First receipt to pay";
            Currency currency = Currency.EUR;
            long lAmount = 10;
            Amount amount = new Amount
            {
                Currency = currency,
                Value = lAmount
            };

            Customer customer = new Customer
            {
                AdditionalProperties = new Dictionary<string, object>
                {
                    { "Name", "Name1" },
                    { "CustomerId", "CustomerId1"}
                }
            };

            PaymentMethod paymentMethod = new PaymentMethod
            {
                Type = PaymentMethodType.WALLET
            };

            QrFormat qrFormat = new QrFormat
            {
                Format = QrFormatFormat.TEXT_TARGETURL
            };

            CreatePaymentRequest paymentRequest = new CreatePaymentRequest
            {
                Amount = amount,
                Customer = customer,
                PaymentDescription = description,
                PaymentMethod = paymentMethod,
                UserFlow = CreatePaymentRequestUserFlow.PUSH_MESSAGE,
            };

            string address = "";
            Uri baseAddres = new Uri(address, UriKind.Absolute);

            TimeSpan paymentTimeout = TimeSpan.FromSeconds(60);

            HttpClient client = new HttpClient
            {
                BaseAddress = baseAddres,
                Timeout = paymentTimeout
            };

            string idempotency_Key = Guid.NewGuid().ToString();
            string ocp_Apim_Subscription_Key = "";
            string merchant_Serial_Number = "";
            string vipps_System_Name = "";
            string vipps_System_Version = "";
            string vipps_System_Plugin_Name = "";
            string vipps_System_Plugin_Version = "";

            EPayment service = new EPayment(client);
            CreatePaymentResponse paymentResponse = await service.CreatePaymentAsync
                (paymentRequest,
                idempotency_Key,
                ocp_Apim_Subscription_Key,
                merchant_Serial_Number,
                vipps_System_Name,
                vipps_System_Version,
                vipps_System_Plugin_Name,
                vipps_System_Plugin_Version);
        }
    }
}
