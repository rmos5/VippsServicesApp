using Context;

namespace VippsServicesApp.Contexts
{
    public partial class LogContext : ContextBase
    {
        public LogContext()
        {
        }

        protected override string SetTitle()
        {
            return "Log";
        }
    }
}
