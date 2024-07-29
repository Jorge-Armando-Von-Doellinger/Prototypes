using System.Reflection.Metadata.Ecma335;
using Gateway.Application.DTOs;
using Gateway.Core.Entity;

namespace Gateway.Application.Map;

public class TransactionMapper
{
    public TransactionEntity Map(TransactionDTO transaction)
    {
        return new TransactionEntity
        {
            Origin = transaction.Origin,
            DataJson = transaction.DataJson,
            Destination = transaction.Destination
        };
    }
}
