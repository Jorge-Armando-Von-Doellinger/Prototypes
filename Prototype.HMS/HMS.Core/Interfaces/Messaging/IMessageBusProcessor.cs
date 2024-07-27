namespace HMS.Core.Interfaces.Messaging
{
    public interface IMessageBusProcessor
    {
        Task ProcessMessage(string routingKey, string bodyJson);
    }
}
