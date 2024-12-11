using MetaCortex.Customers.DataAccess.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MetaCortex.Customers.DataAccess.MessageBroker;

public class MessageConsumerHostedService(IMessageConsumerService service, ILogger<MessageConsumerHostedService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queues = new[] { "order-to-customer", "product-to-customer" };

        foreach (var queue in queues)
        {
            await service.ReadMessageAsync(queue);
        }
        logger.LogInformation($"Message consumer service is running.");
    }
}
