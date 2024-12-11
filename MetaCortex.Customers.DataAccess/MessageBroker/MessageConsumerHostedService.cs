﻿using Microsoft.Extensions.Hosting;

namespace MetaCortex.Customers.DataAccess.MessageBroker;

public class MessageConsumerHostedService(IMessageConsumerService service) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // TODO: Log maybe this and maybe that
        Console.WriteLine($"Message consumer service is running.");
        await service.ReadOrderAsync("order-to-customer");
        await service.ReadNewProductAsync("product-to-customer");
    }
}