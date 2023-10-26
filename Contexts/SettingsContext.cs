using Context;
using System;

namespace VippsServicesApp.Contexts
{
    public partial class SettingsContext : ContextBase
    {
        public SettingsContext(IServiceProvider serviceProvider) : base(serviceProvider) 
        {
        }

        protected override string SetTitle()
        {
            return "Settings";
        }
    }
}
