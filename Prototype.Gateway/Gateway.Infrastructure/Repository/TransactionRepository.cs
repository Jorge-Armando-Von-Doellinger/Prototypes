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
            Console.WriteLine(transaction.DataJson.ToString());
            bool parseSuccess = BsonDocument.TryParse(transaction.DataJson.ToString(), out BsonDocument document);
            if (parseSuccess != false)
            {
                document.Add("origin", transaction.Origin);
                document.Add("destination", transaction.Destination);
                await Task.FromResult(document);
                await _collection.InsertOneAsync(document);
                //return true;
                return true;
            }
            throw new Exception("Erro ao converter os dados da transação!");
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteTransaction(string ID)
    {
        try
        {
            await _collection.DeleteOneAsync(new BsonDocument().Add("_id", ID));
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<TransactionEntity> GetTransactionByID(string ID)
    {
        try
        {
            var document = await _collection.FindAsync(new BsonDocument().Add("_id", ID));
            TransactionEntity transaction = BsonSerializer.Deserialize<TransactionEntity>(document.ToBsonDocument());
            return transaction;
        }
        catch(Exception ex)
        {
            throw ex;
        }
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
