using SeekService.Bill.Interfaces;
using System.Runtime.InteropServices.ObjectiveC;

namespace SeekService.Bill.EmailBillReaders
{
    public interface IEmailBillReader
    {
        public bool Match(IEmailMessage emailMessage);

        public IBillData GetBillData(IEmailMessage emailMessage);
    }
}