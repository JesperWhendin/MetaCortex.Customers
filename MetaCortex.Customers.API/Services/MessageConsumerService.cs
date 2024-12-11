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
    private readonly INotifyCustomerService _notifyCustomerService;

    public MessageConsumerService(IRabbitMqService rabbitMqService, ICheckCustomerStatusService checkCustomerStatusService, INotifyCustomerService notifyCustomerService, ILogger<MessageConsumerService> logger)
    {
        _logger = logger;
        var connection = rabbitMqService.CreateConnection().Result;
        _channel = connection.CreateChannelAsync().Result;

        _checkCustomerStatusService = checkCustomerStatusService;
        _notifyCustomerService = notifyCustomerService;
    }

    public async Task ReadOrderAsync(string queueName)
    {
        _channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).Wait();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
            _logger.LogInformation($"{message} - something");

            await _checkCustomerStatusService.CheckCustomerStatusAsync(message);

        };

        await _channel.BasicConsumeAsync(queue: queueName,
            autoAck: true,
            consumer: consumer);

        await Task.CompletedTask;
    }

    public async Task ReadNewProductAsync(string queueName)
    {
        _channel.QueueDeclareAsync(
            queue: QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        ).Wait();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
            _logger.LogInformation($"{message} - something");

            await _notifyCustomerService.NotifyCustomersAsync(message);

        };

        await _channel.BasicConsumeAsync(queue: queueName,
            autoAck: true,
            consumer: consumer);

        await Task.CompletedTask;
    }
}
