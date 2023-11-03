using Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using VippsServicesApp.Contexts;
using VippsServicesApp.Services;

namespace VippsServicesApp
{
    public partial class App
    {
        private static IHost _host { get; set; }

        private void ConfigureLogging(ILoggingBuilder logging)
        {
            logging.ClearProviders();
            logging.AddDebug();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<VippsServiceSettings>();
            services.AddSingleton<IVippsPaymentService, VippsPaymentService>();
            
            services.AddSingleton<MainContext>();
            services.AddSingleton<SettingsContext>();
            services.AddTransient<PaymentContext>();
            services.AddTransient<CustomerContext>();
            services.AddSingleton<LogContext>();

            services.AddSingleton<IUIService>(this);
        }

        private void StartDI(string[] args)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    ConfigureLogging(logging);
                })
                .ConfigureServices(services =>
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
