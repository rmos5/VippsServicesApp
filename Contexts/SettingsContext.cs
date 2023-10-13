using Context;

namespace VippsServicesApp.Contexts
{
    public partial class SettingsContext : ContextBase
    {
        public SettingsContext()
        {
        }

        protected override string SetTitle()
        {
            return "Settings";
        }
    }
}
