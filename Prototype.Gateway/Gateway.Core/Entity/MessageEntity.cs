using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Core.Entity
{
    public class MessageEntity
    {
        public object Data { get; set; }
        public string RoutingKey { get; set; }
        public string Queue { get; set; }
        public string Exchange { get; set; }
        public string TypeExchange { get; set; }
    }
}
