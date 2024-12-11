namespace MetaCortex.Customers.DataAccess.MessageBroker;

public interface IMessageConsumerService
{
    Task ReadMessageAsync(string queueName);
    //Task ReadNewProductAsync(string queueName);
}
