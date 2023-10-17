using Context;

namespace VippsServicesApp.Contexts
{
    public partial class LogContext : ContextBase
    {
        public LogContext(IUIService uiService) : base(uiService)
        {     
        }

        protected override string SetTitle()
        {
            return "Log";
        }
    }
}
