using VippsServicesApp.Contexts;

namespace VippsServicesApp.Views
{
    /// <summary>
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : ContextView<LogContext>
    {
        public LogView(LogContext context) : base(context)
        {
            InitializeComponent();
        }
    }
}
