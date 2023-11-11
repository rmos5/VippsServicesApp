using Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using VippsServicesApp.Contexts;
using VippsServicesApp.Services;

namespace VippsServicesApp
{
    public partial class App
    {
        private static IHost _host { get; set; }

        private void ConfigureLogging(HostBuilderContext host, IServiceProvider serviceProvider, LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration.ReadFrom.Configuration(host.Configuration);
            string debugOutputTemplate = "[{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";
            loggerConfiguration.WriteTo.Debug(outputTemplate: debugOutputTemplate);
            ILoggingSettings loggingSettings = serviceProvider.GetRequiredService<ILoggingSettings>();
            string logFileName = $"{host.HostingEnvironment.ApplicationName}.txt";
            string loggingFilePath = Path.Combine(loggingSettings.LoggingDirectoryPath, logFileName);
            string fileOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";
            loggerConfiguration.WriteTo.File
                (loggingFilePath,
                encoding: Encoding.UTF8,
                fileSizeLimitBytes: 100000,
                rollOnFileSizeLimit: true,
                rollingInterval: RollingInterval.Hour,
                retainedFileTimeLimit: TimeSpan.FromDays(100),
                outputTemplate: fileOutputTemplate);
        }

        private void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            string loggingDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Logs");
            //string loggingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), host.HostingEnvironment.ApplicationName);//Path.Combine(loggingDirectory, "Logs"); 
            LoggingSettings loggingSettings = new LoggingSettings(loggingDirectory);
            services.AddSingleton<ILoggingSettings>(loggingSettings);

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
                .UseSerilog((host, serviceProvider, loggerConfiguration) =>
                {
                    ConfigureLogging(host, serviceProvider, loggerConfiguration);
                })
                .ConfigureServices((host, services) =>
                {
                    ConfigureServices(host, services);
                })
                .Build();

            Logging.ServiceProvider = _host.Services;
            _host.Start();
        }

        private void StopDI()
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
        }
    }
}
