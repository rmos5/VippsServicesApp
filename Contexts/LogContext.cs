using Context;

namespace VippsServicesApp.Contexts
{
    public partial class LogContext : ContextBase
    {
        protected override string SetTitle()
        {
            return "Log";
        }
    }
}
