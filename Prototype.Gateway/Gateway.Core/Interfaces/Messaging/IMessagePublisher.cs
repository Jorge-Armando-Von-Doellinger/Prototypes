using Gateway.Core.Entity;

namespace Gateway.Core.Interfaces.Messaging
{
    public interface IMessagePublisher
    {
        Task Publish(MessageEntity message);
    }
}
