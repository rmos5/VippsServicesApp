using Context;
using Microsoft.Extensions.Logging;
using System;
using VippsServicesApp.Properties;

namespace VippsServicesApp.Contexts
{
    public partial class MainContext : ContextBase
    {
        private ILogger<MainContext> Logger { get; }

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

        public MainContext(IUIService uiService, ILogger<MainContext> logger, LogContext logContext, SettingsContext settingsContext, PaymentContext paymentContext)
            : base(uiService) 
        {
            LogContext = logContext;
            SettingsContext = settingsContext;
            PaymentContext = paymentContext;
            Logger = logger;
            Logger.LogInformation("MainContext created.");
        }

        protected override string SetTitle()
        {
            return "Vipps Services";
        }

        internal void OnViewClosed()
        {
            if (SelectedIndex == 0)
                SelectedIndex = DefaultIndex;
        }
    }
}
