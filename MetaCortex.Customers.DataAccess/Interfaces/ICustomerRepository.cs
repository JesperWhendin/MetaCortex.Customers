using MetaCortex.Customers.DataAccess.Entities;

namespace MetaCortex.Customers.DataAccess.Interfaces;

public interface ICustomerRepository : IRepository<Customer, string>
{
    Task<Customer> GetByEmailAsync(string email);
    Task<IEnumerable<Customer>> GetByAllowNotificationsAsync();
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(string id);
}