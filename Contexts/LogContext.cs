using Context;
using System;

namespace VippsServicesApp.Contexts
{
    public partial class LogContext : ContextBase
    {
        public LogContext(IServiceProvider serviceProvider) : base(serviceProvider)
        {     
        }

        protected override string SetTitle()
        {
            return "Log";
        }
    }
}
