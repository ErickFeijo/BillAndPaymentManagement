using RegisterServiceAPI.Model;
using RegisterServiceAPI.Service.Interfaces;

namespace RegisterServiceAPI.Service
{
    public class BillService : BaseService<Bill>, IBillService
    {
        public BillService(DbContext context) : base(context)
        {
        }
    }
}
