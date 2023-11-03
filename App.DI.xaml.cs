using Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VippsServicesApp.Contexts;
using VippsServicesApp.Services;

namespace VippsServicesApp
{
    public partial class App
    {
        private static IHost Host { get; set; }

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
            HostApplicationBuilder builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);
            ConfigureServices(builder.Services);
            Host = builder.Build();
            Host.Start();
        }

        private void StopDI()
        {
            Host.StopAsync().GetAwaiter().GetResult();
            Host.Dispose();
        }
    }
}
