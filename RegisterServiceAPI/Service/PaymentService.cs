using RegisterServiceAPI.Messaging.Interfaces;
using RegisterServiceAPI.Model;
using RegisterServiceAPI.Service.Interfaces;
using System.Text.Json;

namespace RegisterServiceAPI.Service
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
