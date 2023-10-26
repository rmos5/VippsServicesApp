using Context;
using System;

namespace VippsServicesApp.Contexts
{
    public partial class CustomerContext : ContextBase
    {
        public CustomerContext(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string SetTitle()
        {
            return "Customer";
        }
    }
}
