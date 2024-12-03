using RabbitMQ.Client;

namespace MetaCortex.Customers.API.Interfaces;

public interface IRabbitMqService
{
    Task<IConnection> CreateConnection();
}