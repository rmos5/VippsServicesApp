using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using VippsServicesApp.Contexts;

namespace VippsServicesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
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
