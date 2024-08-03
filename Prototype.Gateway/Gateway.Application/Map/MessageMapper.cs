using Gateway.Core.Configs;
using Gateway.Core.Entity;

namespace Gateway.Application.Map
{
    public class MessageMapper
    {
        public Task<MessageEntity> MapClient(TransactionEntity transaction)
        {
            return Task.Run(() =>
            {
                return new MessageEntity()
                {
                    Data = transaction.DataJson,
                    Exchange = ClientMessagingSettings.Exchange,
                    Queue = ClientMessagingSettings.QueueName,
                    RoutingKey = transaction.Destination,
                    TypeExchange = ClientMessagingSettings.TypeExchange
                };
            });
        }
    }
}

