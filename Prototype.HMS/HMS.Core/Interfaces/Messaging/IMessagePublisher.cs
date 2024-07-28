namespace HMS.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task Publish(object data, string routingKey);
    }
}
