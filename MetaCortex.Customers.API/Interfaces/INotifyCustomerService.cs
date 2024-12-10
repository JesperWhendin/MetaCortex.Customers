namespace MetaCortex.Customers.API.Interfaces;

public interface INotifyCustomerService
{
    Task NotifyCustomersAsync(string product);
}