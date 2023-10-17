using Context;
using System;
using System.Windows;

namespace VippsServicesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            mainView.DataContext.OnViewClosed();
            base.OnClosed(e);
        }
    }
}