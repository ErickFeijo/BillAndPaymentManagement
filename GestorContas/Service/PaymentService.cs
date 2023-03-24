using GestorContas.Messaging.Interfaces;
using GestorContas.Model;
using GestorContas.Service.Interfaces;
using System.Text.Json;

namespace GestorContas.Service
{
    public class PaymentService : BaseService<Payment>, IPaymentService
    {
        IMessaging _messaging;
        public PaymentService(DbContext context, IMessaging messaging) : base(context)
        {
            _messaging = messaging;
        }

        public override void Add(Payment item)
        {
            base.Add(item);

            _messaging.SendMessage(this.GetType().Name, JsonSerializer.Serialize(item));
        }
    }
}
