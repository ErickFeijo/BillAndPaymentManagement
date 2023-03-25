using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SeekService.Bill.Interfaces;

namespace SeekService.ExternalServices.Email
{
    public class GmailApi : IEmailService
    {
        private readonly string[] _scopes = { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailModify };
        private readonly string _applicationName = "TODO";
        private UserCredential _credential;
        private readonly string _userId = "TODO";

        public async Task<List<IEmailMessage>> GetUnreadEmailsAsync(string label = "INBOX")
        {
            if (_credential == null)
            {
                _credential = await GetUserCredentialAsync();
            }

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });

            var request = service.Users.Messages.List(_userId);
            request.Q = $"is:unread label:{label}"; // only get unread emails with the specified label
            request.MaxResults = 10; // only get up to 10 emails

            var response = await request.ExecuteAsync();

            return new List<IEmailMessage>();

            //return response.Messages; TODO
        }

        public async Task MarkEmailAsReadAsync(string messageId)
        {
            if (_credential == null)
            {
                _credential = await GetUserCredentialAsync();
            }

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = _applicationName,
            });

            var message = new Message();
            message.LabelIds = new List<string>() { "INBOX" };
            var modifyRequest = new ModifyMessageRequest();
            modifyRequest.RemoveLabelIds = new List<string>() { "UNREAD" };
            message.Id = messageId;
            var request = service.Users.Messages.Modify(modifyRequest, _userId, message.Id);
            await request.ExecuteAsync();
        }

        private async Task<UserCredential> GetUserCredentialAsync()
        {
            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    _scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(_applicationName)
                );
                return credential;
            }
        }
    }
}