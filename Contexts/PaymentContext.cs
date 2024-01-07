using Context;
using LoggingHelper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using VippsApi;
using VippsServicesApp.Services;

namespace VippsServicesApp.Contexts
{
    public partial class PaymentContext : ContextBase
    {
        private sealed class PaymentFlowSelectionCommandImpl : CommandBase<PaymentContext>
        {
            public PaymentFlowSelectionCommandImpl(PaymentContext context) : base(context)
            {
            }

            public override bool CanExecute(object parameter)
            {
                return parameter is PaymentFlows
                    && Context.CanExecutePaymentCommand((PaymentFlows)parameter);
            }

            public override void Execute(object parameter)
            {
                Context.ExecutePaymentCommand((PaymentFlows)parameter);
            }
        }

        private ILogger<PaymentContext> Logger { get; }

        private IVippsPaymentService PaymentService { get; }

        public CommandBase PaymentFlowSelectionCommand { get; }

        private PaymentFlows paymentFlow;

        public PaymentFlows PaymentFlow
        {
            get { return paymentFlow; }
            set
            {
                if (paymentFlow != value)
                {
                    paymentFlow = value;
                    OnPropertyChanged(nameof(PaymentFlow));
                    OnPropertyChanged(nameof(IsCustomerQRFlow));
                    OnPropertyChanged(nameof(IsPaymentQRFlow));
                }
            }
        }

        public bool IsCustomerQRFlow => PaymentFlow == PaymentFlows.CustomerQR;

        public bool IsPaymentQRFlow => PaymentFlow == PaymentFlows.PaymentQR;

        private decimal? paymentAmount;

        public decimal? PaymentAmount
        {
            get { return paymentAmount; }
            set
            {
                if (paymentAmount.HasValue && paymentAmount.Value != value
                    || !paymentAmount.HasValue && value.HasValue)
                {
                    paymentAmount = value;
                    OnPropertyChanged(nameof(PaymentAmount));
                }
            }
        }

        public string PaymentAmountText => "Summa...";

        public PaymentContext()
        {
            //note: for UI designer
        }

        public PaymentContext(IUIService uiService, ILogger<PaymentContext> logger, IVippsPaymentService paymentService)
            : base(uiService)
        {
            Log.Debug(this, $"{nameof(PaymentContext)}");
            PaymentService = paymentService;
            Logger = logger;
            PaymentFlowSelectionCommand = new PaymentFlowSelectionCommandImpl(this);
        }

        protected override string SetTitle()
        {
            return "Payment";
        }

        private void UpdateModelState()
        {
            PaymentFlowSelectionCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecutePaymentCommand(PaymentFlows paymentFlow)
        {
            return paymentFlow != PaymentFlow;
        }

        private void ExecutePaymentCommand(PaymentFlows paymentFlow)
        {
            Logger.LogInformation($"{nameof(ExecutePaymentCommand)}:{paymentFlow}...");

            switch (paymentFlow)
            {
                case PaymentFlows.Unknown:
                    throw new NotSupportedException();
                case PaymentFlows.CustomerQR:
                    PaymentFlow = PaymentFlows.CustomerQR;
                    break;
                case PaymentFlows.PaymentQR:
                    PaymentFlow = PaymentFlows.CustomerQR;
                    throw new NotImplementedException();    
                default:
                    break;
            }

            UpdateModelState();
        }

        private void ExecutePayment()
        {
            try
            {
                Logger.LogInformation($"{nameof(ExecutePayment)}...");

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
                Log.Error(this, ex, message);
                UIService.ShowErrorDialog(message, ex, "Payment");
            }
        }
    }
}
