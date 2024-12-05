namespace MetaCortex.Customers.API.Interfaces;

public interface ICheckCustomerStatusService
{
    Task CheckCustomerStatusAsync(string order);
}