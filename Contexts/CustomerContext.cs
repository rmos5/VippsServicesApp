using Context;

namespace VippsServicesApp.Contexts
{
    public partial class CustomerContext : ContextBase
    {
        public CustomerContext(IUIService uiService) : base(uiService)
        {
        }

        protected override string SetTitle()
        {
            return "Customer";
        }
    }
}
