using GestorContas.Model;
using GestorContas.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestorContas.Controllers
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