namespace VippsServicesApp.Services
{
    public interface IServiceFactory
    {
        TService GetService<TService>() where TService : class, IService;
    }
}
