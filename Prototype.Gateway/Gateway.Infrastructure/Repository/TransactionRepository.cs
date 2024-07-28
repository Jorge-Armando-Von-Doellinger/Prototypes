using Gateway.Infrastructure.Collection;
using Gateway.Core.Entity;
using Gateway.Core.Interfaces.Repository;
using Gateway.Core.Responses;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using MongoDB.Bson.Serialization;

namespace Gateway.Infrastructure.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoCollection<BsonDocument> _collection;
    public TransactionRepository(Collections collection)
    {
        _collection = collection.GetDataCollection();
    }

    public async Task<bool> AddTransaction(TransactionEntity transaction)
    {
        try
        {
            var document = BsonDocument.Create(transaction);
            await _collection.InsertOneAsync(document);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> DeleteTransaction(long ID)
    {
        throw new NotImplementedException();
    }

    public Task<TransactionEntity> GetTransactionByID()
    {
        throw new NotImplementedException();
    }

    public async Task<List<TransactionEntity>> GetTransactions()
    {
        try
        {
            var dataDocument = _collection.ToBsonDocument();
            var data = BsonSerializer.Deserialize<List<TransactionEntity>>(dataDocument);
            return await Task.FromResult(data);
        }
        catch
        {
            return default;
        }
    }

    public Task<bool> UpdateTransaction(TransactionEntity transaction)
    {
        throw new NotImplementedException();
    }
}
