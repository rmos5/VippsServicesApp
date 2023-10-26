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
            services.AddSingleton<VippsServiceSettings>();
            services.AddTransient<VippsPaymentService>();
            services.AddTransient((sp) => CreateContext<LogContext>(sp));
            services.AddSingleton((sp) => CreateContext<SettingsContext>(sp));
            services.AddTransient((sp) => CreateContext<PaymentContext>(sp));
            services.AddTransient((sp) => CreateContext<CustomerContext>(sp));
            services.AddSingleton((sp) => CreateContext<MainContext>(sp));
            services.AddSingleton<IUIService>(this);
        }

        private T CreateContext<T>(IServiceProvider serviceProvider)
            where T: ContextBase, new()
        {
            T result = new T();
            result.ServiceProvider = serviceProvider;
            return result;
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
