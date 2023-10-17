using System.Threading.Tasks;
using VippsApi;

namespace VippsServicesApp.Services
{
    public interface IPaymentService<TPayment, TPaymentResponse> : IService
    {
        CreatePaymentResponse DoPayment(CreatePaymentRequest paymentRequest);
    }
}
