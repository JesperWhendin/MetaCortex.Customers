using Microsoft.Extensions.Hosting;

namespace MetaCortex.Customers.DataAccess.MessageBroker;

public class MessageConsumerHostedService(IMessageConsumerService service) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await service.ReadMessageAsync();
    }
}