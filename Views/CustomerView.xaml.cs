using System.Windows.Controls;
using VippsServicesApp.Contexts;

namespace VippsServicesApp.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : ContextViewBase<CustomerContext>
    {
        public CustomerView()
        {
            InitializeComponent();
        }
    }
}
