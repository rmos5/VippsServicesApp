using Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Extensions;
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

        private ILogger<PaymentContext> Logger { get; }

        private IVippsPaymentService PaymentService { get; }

        public CommandBase PaymentCommand { get; }

        public PaymentContext()
        {
            //note: for UI designer
        }

        public PaymentContext(IUIService uiService, ILogger<PaymentContext> logger, IVippsPaymentService paymentService)
            : base(uiService)
        {
            Logging.Debug(this, $"{nameof(PaymentContext)}");
            PaymentService = paymentService;
            Logger = logger;
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
                Logger.LogInformation($"{nameof(ExecutePaymentCommand)}...");

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
                    UserFlow = CreatePaymentRequestUserFlow.QR,
                };

                CreatePaymentResponse response = PaymentService.RequestPayment(paymentRequest);
            }
            catch (Exception ex)
            {
                string message = "Payment failed.";
                Logging.Error(this, ex, message);
                UIService.ShowErrorDialog(message, ex, "Payment");
            }
        }
    }
}
