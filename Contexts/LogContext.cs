using Context;

namespace VippsServicesApp.Contexts
{
    public partial class LogContext : ContextBase
    {
        public LogContext()
        {
            //note: for UI designer
        }

        public LogContext(IUIService uiService)
            :base(uiService)
        {
        }

        protected override string SetTitle()
        {
            return "Log";
        }
    }
}
