using SeekService.Bill.EmailBillReaders;
using SeekService.Bill.Interfaces;
using SeekService.ExternalServices.Email;

namespace SeekService.Bill
{
    public class BillFactory : IBillFactory
    {
        IEmailService _emailService;
        IEnumerable<IEmailBillReader> _billReaders;

        public BillFactory(IEmailService emailService, IEnumerable<IEmailBillReader> billReaders)
        {
            _emailService = emailService;
            _billReaders = billReaders;
        }

        public async Task<List<IBillData>> GetAllNewBills()
        {
            List<IBillData> bills = new List<IBillData>();

            List<IEmailMessage> emailMessages = await _emailService.GetUnreadEmailsAsync("Bill");

            foreach (IEmailMessage message in emailMessages)
            {
                IBillData billData = _billReaders.FirstOrDefault(x => x.Match(message)).GetBillData(message);

                bills.Add(billData);

                await _emailService.MarkEmailAsReadAsync(message.Id);
            }

            return bills;
        }
    }
}
