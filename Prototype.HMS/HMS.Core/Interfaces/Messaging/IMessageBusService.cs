using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Core.Interfaces.Messaging
{
    public interface IMessageBusService
    {
        Task Publish(object data, string routingKey);
        Task StartListener();
    }
}
