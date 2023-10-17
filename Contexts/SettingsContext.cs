using Context;

namespace VippsServicesApp.Contexts
{
    public partial class SettingsContext : ContextBase
    {
        public SettingsContext(IUIService uiService) : base(uiService)
        {
        }

        protected override string SetTitle()
        {
            return "Settings";
        }
    }
}
