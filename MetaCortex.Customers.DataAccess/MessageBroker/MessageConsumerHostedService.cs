using Microsoft.Extensions.Hosting;

namespace MetaCortex.Customers.DataAccess.MessageBroker;

public class MessageConsumerHostedService(IMessageConsumerService service) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: Log maybe this and maybe that
        var queues = new[] { "order-to-customer", "product-to-customer" };

        foreach (var queue in queues)
        {


            await service.ReadMessageAsync(queue);



        }


        //await service.ReadOrderAsync("order-to-customer");
        //await service.ReadNewProductAsync("product-to-customer");

        Console.WriteLine($"Message consumer service is running.");
    }
}