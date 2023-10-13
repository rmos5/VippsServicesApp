using Context;

namespace VippsServicesApp.Contexts
{
    public partial class CustomerContext : ContextBase
    {
        public CustomerContext()
        {
        }

        protected override string SetTitle()
        {
            return "Customer";
        }
    }
}
