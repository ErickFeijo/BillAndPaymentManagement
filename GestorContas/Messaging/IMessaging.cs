using System.Threading.Channels;

namespace GestorContas.Messaging.Interfaces
{
    public interface IMessaging
    {
        public void SendMessage(string queueName, string message);
        public void ReceiveMessage(string queueName, Action<string> messageHandler);
    }
}