using System.Text.Json;
using MetaCortex.Customers.API.DTOs;
using MetaCortex.Customers.DataAccess.Interfaces;

namespace MetaCortex.Customers.API.Services;

//public class NotifyCustomerService
//{
//    private readonly ICustomerRepository _customerRepository;

//    public NotifyCustomerService(ICustomerRepository repo)
//    {
//        _customerRepository = repo;
//    }

//    public async Task NotifyCustomersAsync(string product)
//    {
//        var productDto = JsonSerializer.Deserialize<ProductDto>(product);

//        if (productDto is null)
//            throw new ArgumentNullException(nameof(productDto));

//        // Send info to existing customers about new available product via e.g. email

//    }
//}