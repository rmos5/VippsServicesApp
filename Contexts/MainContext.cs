using Context;
using Microsoft.Extensions.DependencyInjection;
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

        public LogContext LogContext => ServiceProvider.GetRequiredService<LogContext>();
        public SettingsContext SettingsContext => ServiceProvider.GetRequiredService<SettingsContext>();
        public PaymentContext PaymentContext => ServiceProvider.GetRequiredService<PaymentContext>();

        public MainContext(IServiceProvider serviceProvider) : base(serviceProvider)
        {
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
