using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Application.Map
{
    public class Mapper
    {
        public TransactionMapper TransactionMapper { get; }
        public MessageMapper MessageMapper { get; }
        public Mapper(TransactionMapper transactionMapper, MessageMapper messageMapper) 
        { 
            MessageMapper = messageMapper;
            TransactionMapper = transactionMapper;
        }
    }
}
