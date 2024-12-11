using System.Text;
using System.Text.Json;
using MetaCortex.Customers.DataAccess.MessageBroker;
using RabbitMQ.Client;

namespace MetaCortex.Customers.API.Services;

public class MessageProducerService(RabbitMqConfiguration config)
    : IMessageProducerService
{
    private readonly ConnectionFactory _connection = new()
    {
        HostName = config.HostName,
        UserName = config.UserName,
        Password = config.Password,
        VirtualHost = "/"
    };

    public async Task SendMessageAsync<T>(T message, string queueName)
    {
        await using var connection = await _connection.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        await channel.BasicPublishAsync(exchange: "",
            routingKey: queueName,
            body: body);
    }
}
