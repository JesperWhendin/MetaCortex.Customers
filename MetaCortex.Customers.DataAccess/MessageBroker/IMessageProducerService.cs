namespace MetaCortex.Customers.DataAccess.MessageBroker;

public interface IMessageProducerService
{
    Task SendMessageAsync<T>(T message);
}