using System;
using System.Windows;
using VippsServicesApp.Contexts;

namespace VippsServicesApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public new MainContext DataContext
        {
            get => (MainContext)base.DataContext;
            set => base.DataContext = value;
        }

        public MainWindow(MainContext context)
        {
            DataContext = context ?? throw new ArgumentNullException(nameof(context));
            InitializeComponent();
        }
    }
}