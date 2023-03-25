using SeekService.Bill.Interfaces;
using SeekService.Messaging.Interfaces;
using System.Text.Json;

namespace SeekService.Bill
{
    public class BillWorker : BackgroundService
    {
        private readonly IBillFactory _billFactory;
        private readonly IMessaging _messaging;
        private readonly ILogger<BillWorker> _logger;

        public BillWorker(IBillFactory billFactory, IMessaging messaging, ILogger<BillWorker> logger)
        {
            _billFactory = billFactory;
            _messaging = messaging;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation($"{this.GetType().Name} running at: {DateTimeOffset.Now}");

                    await Run(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{this.GetType().Name} error: {ex.Message}");
            }
        }

        private async Task Run(CancellationToken stoppingToken)
        {
            List<IBillData> billList = await _billFactory.GetAllNewBills();

            billList.ForEach(bill =>
            {
                _messaging.SendMessage(this.GetType().Name, JsonSerializer.Serialize(bill));
            });

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}
