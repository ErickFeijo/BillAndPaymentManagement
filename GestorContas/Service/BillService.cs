using GestorContas.Model;
using GestorContas.Service.Interfaces;

namespace GestorContas.Service
{
    public class BillService : BaseService<Bill>, IBillService
    {
        public BillService(DbContext context) : base(context)
        {
        }
    }
}
