using System.Text.Json;
using MetaCortex.Customers.API.DTOs;
using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.DataAccess.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;

namespace MetaCortex.Customers.API.Services;

public class NotifyCustomerService(ICustomerRepository repo, ILogger<NotifyCustomerService> logger)
    : INotifyCustomerService
{
    public async Task NotifyCustomersAsync(string product)
    {
        var productDto = JsonSerializer.Deserialize<ProductDto>(product);

        if (productDto is null)
            throw new ArgumentNullException(nameof(productDto));

        var customers = await repo.GetByAllowNotificationsAsync();

        if (!customers.Any())
            logger.LogInformation("No customers that allow notifications found.");

        if (customers is not null)
        {
            foreach (var customer in customers)
            {
                logger.LogInformation($"logger.LogInformation: Nu finns {productDto.Name} i sortimentet.");
            }
        }
    }
}
