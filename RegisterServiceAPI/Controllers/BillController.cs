using RegisterServiceAPI.Model;
using RegisterServiceAPI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RegisterServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : BaseController<Bill>
    {
        public BillController(IBillService billService) : base(billService)
        {
        }
   }
}