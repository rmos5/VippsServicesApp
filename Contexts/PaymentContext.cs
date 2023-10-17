using Context;
using System;
using System.Collections.Generic;
using VippsApi;
using VippsServicesApp.Services;

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

            public override void Execute(object parameter)
            {
                Context.ExecutePaymentCommand(parameter);
            }
        }

        protected VippsServiceSettings Settings { get; }

        protected IServiceFactory ServiceFactory { get; }

        public CommandBase PaymentCommand { get; }

        public PaymentContext(IUIService uiService, IServiceFactory serviceFactory) : base(uiService)
        {
            ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
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

        private void ExecutePaymentCommand(object parameter)
        {
            try
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

                IVippsPaymentService service = ServiceFactory.GetService<IVippsPaymentService>();
                service.DoPayment(paymentRequest);
            }
            catch (Exception ex)
            {
                UIService.ShowErrorDialog("Payment failed.", ex, "Payment");
            }
        }
    }
}
