using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VippsServicesApp.Contexts;
using VippsServicesApp.Views;

namespace VippsServicesApp
{
    public partial class App
    {
        private static IHost _host;

        private void StartDI()
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<LogContext>();
                services.AddSingleton<SettingsContext>();
                services.AddTransient<PaymentContext>();
                services.AddTransient<CustomerContext>();
                services.AddSingleton<MainContext>();
            });

            _host = builder.Build();
            _host.Start();
        }

        private void StopDI()
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
        }
    }
}
