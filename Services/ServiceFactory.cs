using Microsoft.Extensions.DependencyInjection;
using System;

namespace VippsServicesApp.Services
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ServiceFactory(IServiceProvider serviceProvider)
        {
           _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        TService IServiceFactory.GetService<TService>()
        {
            return _serviceProvider.GetRequiredService<TService>();
        }
    }
}
