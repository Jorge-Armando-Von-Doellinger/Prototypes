using Gateway.Core.Entity;
using Gateway.Core.Interfaces.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Application.Services
{
    public class MessageService
    {
        private readonly IMessagePublisher _messagePublisher;
        public MessageService(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }
        public async Task PublishMessage(MessageEntity message)
        {
            await _messagePublisher.Publish(message);
        }
    }
}
