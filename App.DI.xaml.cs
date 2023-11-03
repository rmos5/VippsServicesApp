using Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using VippsServicesApp.Contexts;
using VippsServicesApp.Services;

namespace VippsServicesApp
{
    public partial class App
    {
        private static IHost _host { get; set; }

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
            _host = Host.CreateDefaultBuilder(args)
                .UseSerilog((host, logConfig) =>
                {
                    logConfig.WriteTo.Debug(LogEventLevel.Debug);
                    logConfig.WriteTo.File("Logs/VippsServicesApp.log", LogEventLevel.Debug, rollingInterval: RollingInterval.Hour);
                })
                .ConfigureServices(services =>
                {
                    ConfigureServices(services);
                })
                .Build();
            _host.Start();
        }

        private void StopDI()
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
        }
    }
}
