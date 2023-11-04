﻿using Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Extensions;
using System.Security.Principal;
using System.Threading;
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
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            identity.Impersonate();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            Thread.CurrentPrincipal = principal;

            StartDI(e.Args);
            MainWindow = new MainWindow();
            MainWindow.DataContext = _host.Services.GetRequiredService<MainContext>();
            MainWindow.Closing += MainWindow_Closing;
            MainWindow.Show();
        }

        public void ShowErrorDialog(string message, Exception exception, string dialogTitle)
        {
            MessageBox.Show(MainWindow, $"{message}\n{exception.GetAllMessages()}", dialogTitle, MessageBoxButton.OK, MessageBoxImage.Error);
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
