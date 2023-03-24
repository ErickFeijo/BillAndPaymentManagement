using GestorContas.Model;
using GestorContas.Service.Interfaces;

namespace GestorContas.Service
{
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        public PaymentService(DbContext context) : base(context)
        {
        }
    }
}
