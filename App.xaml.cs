using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using VippsServicesApp.Contexts;
using VippsServicesApp.Views;

namespace VippsServicesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainContext MainContext => _host.Services.GetService<MainContext>();

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<LogContext>();
                services.AddSingleton<SettingsContext>();
                services.AddSingleton<PaymentContext>();
                services.AddTransient<CustomerContext>();
                services.AddSingleton<MainContext>();
                services.AddSingleton<MainWindow>();
            });
            
            _host = builder.Build();
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
