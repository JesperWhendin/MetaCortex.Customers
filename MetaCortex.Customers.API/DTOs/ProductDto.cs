using MetaCortex.Customers.DataAccess.Entities;

namespace MetaCortex.Customers.API.DTOs;

public class ProductDto : EntityBase
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}