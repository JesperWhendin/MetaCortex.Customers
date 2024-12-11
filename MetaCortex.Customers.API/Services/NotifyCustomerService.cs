using System.Text.Json;
using MetaCortex.Customers.API.DTOs;
using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.DataAccess.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;

namespace MetaCortex.Customers.API.Services;

public class NotifyCustomerService(ICustomerRepository repo, ILogger<NotifyCustomerService> logger)
    : INotifyCustomerService
{
    private const string queueName = "product-to-customer"; // Lär inte behövas nå jag inte skickar tillbaka något på c-t-p

    public async Task NotifyCustomersAsync(string product)
    {
        var productDto = JsonSerializer.Deserialize<ProductDto>(product);

        if (productDto is null)
            throw new ArgumentNullException(nameof(productDto));

        var customers = await repo.GetByAllowNotificationsAsync();

        foreach (var customer in customers)
        {

            Console.WriteLine($"Console.WriteLine: Nu finns {productDto.Name} i sortimentet.");
            logger.LogInformation($"logger.LogInformation: Nu finns {productDto.Name} i sortimentet.");
        }
    }
}
