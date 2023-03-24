using GestorContas.Messaging.Interfaces;
using RabbitMQ.Client;

public class RabbitMQService : IMessaging
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService(RabbitMQConfig options)
    {
        var factory = new ConnectionFactory() { HostName = options.HostName, UserName = options.User, Password = options.Password };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void SendMessage(string queueName, string message)
    {
        _channel.QueueDeclare(queue: queueName, 
            durable: false, 
            exclusive: false, 
            autoDelete: false, 
            arguments: null);

        var body = System.Text.Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "", 
            routingKey: queueName, 
            basicProperties: null, 
            body: body);
    }

    public void ReceiveMessage(string queueName, Action<string> messageHandler)
    {
        _channel.QueueDeclare(queue: queueName, 
            durable: false, 
            exclusive: false, 
            autoDelete: false, 
            arguments: null);

        var consumer = new RabbitMQ.Client.Events.EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = System.Text.Encoding.UTF8.GetString(body);
            messageHandler(message);
        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
}