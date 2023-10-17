using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VippsServicesApp.Services
{
    public interface IPaymentService<TPayment, TVoidPayment>
    {
        void DoPayment(TPayment payment);
        void VoidPayment(TVoidPayment payment);
    }
}
