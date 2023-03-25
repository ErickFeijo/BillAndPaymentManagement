using SeekService.Bill.Interfaces;

namespace SeekService.Bill.EmailBillReaders
{
    public class CreditCardBillInterEmailReader : IEmailBillReader
    {
        public IBillData GetBillData(IEmailMessage emailMessage)
        {
            throw new NotImplementedException();
        }

        public bool Match(IEmailMessage emailMessage)
        {
            throw new NotImplementedException();
        }
    }
}
