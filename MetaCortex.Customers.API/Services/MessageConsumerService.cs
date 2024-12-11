using System.Text;
using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MetaCortex.Customers.API.Services;

public class MessageConsumerService : IMessageConsumerService
{
    private readonly IChannel _channel;
    private readonly ILogger<MessageConsumerService> _logger;

    private const string QueueName = "order-to-customer";

    private readonly ICheckCustomerStatusService _checkCustomerStatusService;

    public MessageConsumerService(IRabbitMqService rabbitMqService, ICheckCustomerStatusService checkCustomerStatusService, ILogger<MessageConsumerService> logger)
    {
        _logger = logger;
        var connection = rabbitMqService.CreateConnection().Result;
        _channel = connection.CreateChannelAsync().Result;
        _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).Wait();
        _checkCustomerStatusService = checkCustomerStatusService;
    }

    public async Task ReadMessageAsync()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
            _logger.LogInformation($"ETT SKEPP KOMMER LASTAT MED LITE DANK LOGS");
            await _checkCustomerStatusService.CheckCustomerStatusAsync(message);
        };

        await _channel.BasicConsumeAsync(queue: QueueName,
            autoAck: true,
            consumer: consumer);

        await Task.CompletedTask;
    }
}
