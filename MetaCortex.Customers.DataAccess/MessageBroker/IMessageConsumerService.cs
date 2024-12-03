namespace MetaCortex.Customers.DataAccess.MessageBroker;

public interface IMessageConsumerService
{
    Task ReadMessageAsync();
}