using GestorContas.Model;
using GestorContas.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorContas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : BaseController<Payment>
    {
        public PaymentController(IBaseService<Payment> _baseService) : base(_baseService)
        {
        }
    }
}