namespace MetaCortex.Customers.DataAccess.MessageBroker;

public interface IMessageConsumerService
{
    Task ReadOrderAsync(string queueName);
    Task ReadNewProductAsync(string queueName);
}
