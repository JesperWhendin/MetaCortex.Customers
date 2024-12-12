using System.Text.Json;
using MetaCortex.Customers.API.DTOs;
using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.DataAccess.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;

namespace MetaCortex.Customers.API.Services;

public class CheckCustomerStatusService(ICustomerRepository repo, IMessageProducerService msg, ILogger<CheckCustomerStatusService> logger)
    : ICheckCustomerStatusService
{
    private const string queueName = "customer-to-order";

    public async Task CheckCustomerStatusAsync(string order)
    {
        logger.LogInformation("Order received.");
        var orderDto = JsonSerializer.Deserialize<OrderDto>(order);

        if (orderDto.CustomerId is null)
        {
            logger.LogInformation("Customer id is null.");
            await msg.SendMessageAsync(orderDto, queueName);
            return;
        }
        //throw new SystemException("Invalid customer id.");

        logger.LogInformation($"Retrieving customer with id {orderDto.CustomerId}.");
        var customer = await repo.GetByIdAsync(orderDto.CustomerId);

        logger.LogInformation("Setting vip status for order equal to customer status.");
        orderDto.VIPStatus = customer.IsVip;

        logger.LogInformation("Sending order to message broker.");
        await msg.SendMessageAsync(orderDto, queueName);
    }
}
