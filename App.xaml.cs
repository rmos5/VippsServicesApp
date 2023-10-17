using Context;
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
        private MainContext MainContext { get; set; }

        public App()
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            StartDI();
            MainContext = _host.Services.GetRequiredService<MainContext>();
            MainWindow = new MainWindow();
            MainWindow.DataContext = MainContext;
            MainWindow.Closing += MainWindow_Closing;
            MainWindow.Show();
        }


        public void ShowErrorDialog(string message, Exception exception, string dialogTitle)
        {
            MessageBox.Show(MainWindow, $"{message}\n{exception.Message}", dialogTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            
        }

        protected override void OnExit(ExitEventArgs e)
        {
            StopDI();
            base.OnExit(e);
        }

    }
}
