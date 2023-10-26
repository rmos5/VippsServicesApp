using Context;

namespace VippsServicesApp.Contexts
{
    public partial class SettingsContext : ContextBase
    {
        protected override string SetTitle()
        {
            return "Settings";
        }
    }
}
