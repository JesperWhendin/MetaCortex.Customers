using System.Text.Json;
using MetaCortex.Customers.API.DTOs;
using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.DataAccess.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;

namespace MetaCortex.Customers.API.Services;

public class CheckCustomerStatusService(ICustomerRepository repo, IMessageProducerService msg)
    : ICheckCustomerStatusService
{
    private const string queueName = "customer-to-order";

    public async Task CheckCustomerStatusAsync(string order)
    {
        var orderDto = JsonSerializer.Deserialize<OrderDto>(order);

        if (orderDto.CustomerId is null)
            throw new SystemException("Invalid customer id.");


        var customer = await repo.GetByIdAsync(orderDto.CustomerId);
        orderDto.VIPStatus = customer.IsVip;
        await msg.SendMessageAsync(orderDto, queueName);
    }
}
