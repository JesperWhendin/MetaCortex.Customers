using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;
using RabbitMQ.Client;

namespace MetaCortex.Customers.API.Services;

public class RabbitMqService(RabbitMqConfiguration config) : IRabbitMqService
{
    public Task<IConnection> CreateConnection()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = config.HostName,
            UserName = config.UserName,
            Password = config.Password
        };
        var connection = connectionFactory.CreateConnectionAsync();
        return connection;
    }
}