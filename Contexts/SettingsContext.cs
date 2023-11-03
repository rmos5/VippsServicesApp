using Context;

namespace VippsServicesApp.Contexts
{
    public partial class SettingsContext : ContextBase
    {

        public SettingsContext()
        {
            //note: for UI designer
        }

        public SettingsContext(IUIService uiService)
            : base(uiService)
        {
        }

        protected override string SetTitle()
        {
            return "Settings";
        }
    }
}
