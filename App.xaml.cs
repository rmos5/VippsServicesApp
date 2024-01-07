using Context;
using LoggingHelper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Windows;
using VippsServicesApp.Contexts;

namespace VippsServicesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IUIService
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _host.Services.GetRequiredService<IUIService>().ShowErrorDialog(e.Exception, "Unhandled error");
            e.Handled = true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //WindowsIdentity identity = WindowsIdentity.GetCurrent();
            //identity.Impersonate();
            //WindowsPrincipal principal = new WindowsPrincipal(identity);
            //Thread.CurrentPrincipal = principal;

            StartDI(e.Args);
            Log.Trace(this, $"{nameof(OnStartup)}");
            MainWindow = new MainWindow();
            MainWindow.DataContext = _host.Services.GetRequiredService<MainContext>();
            MainWindow.Closing += MainWindow_Closing;
            MainWindow.Show();
            Log.Information(this, $"Application started.");
        }

        public void ShowErrorDialog(string message, Exception exception, string dialogTitle)
        {
            string msg;
            if (message?.Trim()?.Length > 0)
                msg = $"{message}\n{exception.GetAllMessages()}";
            else
                msg = $"{exception.GetAllMessages()}";

            MessageBox.Show(MainWindow, msg, dialogTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowErrorDialog(Exception exception, string dialogTitle)
        {
            ShowErrorDialog(null, exception, dialogTitle);
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Log.Trace(this, $"{nameof(MainWindow_Closing)}:Cancel={e.Cancel}");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information(this, "Application exit.");
            StopDI();
            base.OnExit(e);
        }
    }
}
