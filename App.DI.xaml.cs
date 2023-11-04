using Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using VippsServicesApp.Contexts;
using VippsServicesApp.Services;

namespace VippsServicesApp
{
    public partial class App
    {
        private static IHost _host { get; set; }

        private void ConfigureLogging(HostBuilderContext host, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration.MinimumLevel.Verbose();
            loggerConfiguration.WriteTo.Debug();
            loggerConfiguration.WriteTo.File("Logs/VippsServicesApp.log", fileSizeLimitBytes: 100000, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Hour, retainedFileTimeLimit: TimeSpan.FromDays(100));
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
            _host = Host.CreateDefaultBuilder(args)
                .UseSerilog((host, loggerConfiguration) =>
                {
                    ConfigureLogging(host, loggerConfiguration);
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
