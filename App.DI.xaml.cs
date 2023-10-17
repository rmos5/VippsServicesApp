using Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VippsServicesApp.Contexts;
using VippsServicesApp.Services;

namespace VippsServicesApp
{
    public partial class App
    {
        private static IHost _host;

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IServiceFactory, ServiceFactory>();
            services.AddSingleton<VippsServiceSettings>();
            services.AddTransient<IVippsPaymentService, VippsPaymentService>();
            services.AddTransient<LogContext>();
            services.AddSingleton<SettingsContext>();
            services.AddTransient<PaymentContext>();
            services.AddTransient<CustomerContext>();
            services.AddSingleton<MainContext>();
            services.AddSingleton<IUIService>(this);
        }

        private IUIService GetUIService(IServiceProvider provider)
        {
            return MainWindow as IUIService;
        }

        private void StartDI()
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices((hostContext, services) =>
            {
                ConfigureServices(services);
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
