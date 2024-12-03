using System.Text;
using MetaCortex.Customers.DataAccess.MessageBroker;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MetaCortex.Customers.API.Services;

public class MessageConsumerService : IMessageConsumerService
{
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    public MessageConsumerService(RabbitMqConfiguration rabbitMqConfiguration)
    {
        _rabbitMqConfiguration = rabbitMqConfiguration;
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfiguration.HostName,
            UserName = _rabbitMqConfiguration.UserName,
            Password = _rabbitMqConfiguration.Password
        };

        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
    }
    public async Task ReadMessageAsync()
    {
        await _channel.QueueDeclareAsync(queue: "customer",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };

        await _channel.BasicConsumeAsync(queue: "customer",
            autoAck: true,
            consumer: consumer);

        await Task.CompletedTask;
    }
}