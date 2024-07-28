namespace HMS.Core.Interfaces.Messaging
{
    public interface IMessageBusProcessor
    {
        Task ProcessMessage(string routingKey, byte[] message);
    }
}
