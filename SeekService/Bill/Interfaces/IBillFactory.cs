
namespace SeekService.Bill.Interfaces
{
    public interface IBillFactory
    {
        Task<List<IBillData>> GetAllNewBills();
    }
}