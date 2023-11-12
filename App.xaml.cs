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
            MessageBox.Show(MainWindow, $"{message}\n{exception.GetAllMessages()}", dialogTitle, MessageBoxButton.OK, MessageBoxImage.Error);
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
