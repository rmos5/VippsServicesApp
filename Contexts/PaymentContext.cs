using Context;

namespace VippsServicesApp.Contexts
{
    public partial class PaymentContext : ContextBase
    {
        public PaymentContext()
        {
        }

        protected override string SetTitle()
        {
            return "Payment";
        }
    }
}
