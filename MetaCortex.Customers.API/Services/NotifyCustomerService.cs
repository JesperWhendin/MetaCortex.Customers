using System.Text.Json;
using MetaCortex.Customers.API.DTOs;
using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.DataAccess.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;

namespace MetaCortex.Customers.API.Services;

public class NotifyCustomerService(ICustomerRepository repo, IMessageProducerService msg)
    : INotifyCustomerService
{
    private const string queueName = "product-to-customer";

    public async Task NotifyCustomersAsync(string product)
    {
        var productDto = JsonSerializer.Deserialize<ProductDto>(product);

        if (productDto is null)
            throw new ArgumentNullException(nameof(productDto));

        // Send info to existing customers about new available product via e.g. email

        var customers = await repo.GetByAllowNotificationsAsync();

        foreach (var customer in customers)
        {
            //EmailService.SendEmailNotification(productDto);
            Console.WriteLine($"Nu finns {productDto.Name} i sortimentet.");
        }
        // Hur blir logiken för detta, ska msg.Send.. vara med?
        // await msg.SendMessageAsync(productDto, queueName);
    }
}