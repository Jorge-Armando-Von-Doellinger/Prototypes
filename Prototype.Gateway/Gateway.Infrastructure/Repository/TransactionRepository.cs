using Gateway.Infrastructure.Collection;
using Gateway.Core.Entity;
using Gateway.Core.Interfaces.Repository;
using Gateway.Core.Responses;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.Json;
using MongoDB.Bson.Serialization;
using System.Text.Json.Nodes;
using MongoDB.Bson.IO;
using System.Text.Json.Serialization;
using Amazon.Runtime.Internal.Transform;

namespace Gateway.Infrastructure.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoCollection<BsonDocument> _collection;

    private const string ErrorDataConvert = "Erro ao converter os dados da transação!";

    public TransactionRepository(Collections collection)
    {
        _collection = collection.GetDataCollection();
    }

    public async Task<bool> AddTransaction(TransactionEntity transaction)
    {
        try
        {
            //Console.WriteLine(transaction.DataJson.ToString());
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
            throw new Exception(ErrorDataConvert);
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

    public async Task<JsonObject> GetTransactionByID(string ID)
    {
        try
        {
            var document = await _collection.FindAsync(new BsonDocument().Add("_id", ID));
            return await Task.FromResult(JsonSerializer.Deserialize<JsonObject>(document.ToJson()));
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<JsonObject>> GetTransactions()
    {
        try
        {
            string dataString = _collection
                .FindAsync(Builders<BsonDocument>.Filter.Empty)
                .ToJson(new JsonWriterSettings { Indent = true });
            await Task.FromResult(dataString);
            List<JsonObject> dataJson = JsonSerializer.Deserialize<List<JsonObject>>(dataString);
            return await Task.FromResult(dataJson);
        }
        catch
        {
            return default;
        }
    }

    public async Task<bool> UpdateTransaction(TransactionEntity transaction)
    {
        try
        {
            var dataParseSucess = BsonDocument.TryParse(transaction.DataJson.ToString(), out BsonDocument data);
            if(dataParseSucess)
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", transaction.TransactionId);
                var update = new BsonDocument { { "$set", data } };
                await _collection.UpdateOneAsync(filter, update);
                return true;
            }
            throw new Exception(ErrorDataConvert);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
