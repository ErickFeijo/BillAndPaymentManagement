using SeekService.Bill.Interfaces;

namespace SeekService.ExternalServices.Email
{
    public interface IEmailService
    {
        public Task<List<IEmailMessage>> GetUnreadEmailsAsync(string? label);
        public Task MarkEmailAsReadAsync(string messageId);
    }
}
