using Context;
using System;
using VippsServicesApp.Properties;

namespace VippsServicesApp.Contexts
{
    public partial class MainContext : ContextBase
    {
        private int defaultIndex = Settings.Default.MainViewDefaultIndex;
        public int DefaultIndex
        {
            get => defaultIndex;
            set
            {
                if (value != defaultIndex)
                {
                    defaultIndex = value;
                    Settings.Default.MainViewDefaultIndex = (int)value;
                    OnPropertyChanged();
                }
            }
        }

        private int selectedtIndex = Settings.Default.MainViewDefaultIndex;
        public int SelectedIndex
        {
            get => selectedtIndex;
            set
            {
                if (value != selectedtIndex)
                {
                    selectedtIndex = value;
                    Settings.Default.MainViewDefaultIndex = (int)value;
                    OnPropertyChanged();
                }
            }
        }

        public LogContext LogContext { get; }
        public SettingsContext SettingsContext { get; }
        public PaymentContext PaymentContext { get; }

        public MainContext(IUIService uiService, LogContext logContext, SettingsContext settingsContext, PaymentContext paymentContext) : base(uiService)
        {
            LogContext = logContext ?? throw new ArgumentNullException(nameof(logContext));
            SettingsContext = settingsContext ?? throw new ArgumentNullException(nameof(settingsContext));
            PaymentContext = paymentContext ?? throw new ArgumentNullException(nameof(paymentContext));
        }

        protected override string SetTitle()
        {
            return "Vipps Services";
        }

        internal void OnViewClosed()
        {
            if (SelectedIndex == 0
                || SelectedIndex == 1)
                SelectedIndex = DefaultIndex;
        }
    }
}
