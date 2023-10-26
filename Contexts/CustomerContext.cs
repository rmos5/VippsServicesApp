using Context;

namespace VippsServicesApp.Contexts
{
    public partial class CustomerContext : ContextBase
    {
        protected override string SetTitle()
        {
            return "Customer";
        }
    }
}
