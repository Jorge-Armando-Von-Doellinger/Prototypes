namespace Gateway.Core.Interfaces.Messaging
{
    public interface IMessageListener
    {
        Task StartListener(string queue);
    }
}
