using RegisterServiceAPI.Model;
using RegisterServiceAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RegisterServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : BaseController<Payment>
    {
        public PaymentController(IPaymentService paymentService) : base(paymentService)
        {
        }
    }
}