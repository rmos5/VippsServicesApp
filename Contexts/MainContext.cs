using Context;
using System.Diagnostics;
using System.Threading;
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
                    Settings.Default.MainViewDefaultIndex = value;
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

        public MainContext()
        {
            //note: for UI designer
        }

        public MainContext(IUIService uiService, LogContext logContext, SettingsContext settingsContext, PaymentContext paymentContext)
            : base(uiService) 
        {
            Logging.Debug<MainContext>($"{nameof(MainContext)}");
            LogContext = logContext;
            SettingsContext = settingsContext;
            PaymentContext = paymentContext;
        }

        protected override string SetTitle()
        {
            return $"Vipps Services ({Thread.CurrentPrincipal.Identity.Name})";
        }

        internal void OnViewClosed()
        {
            Logging.Information<MainContext>("Main view closed.");

            if (SelectedIndex == 0)
                SelectedIndex = DefaultIndex;
        }
    }
}
