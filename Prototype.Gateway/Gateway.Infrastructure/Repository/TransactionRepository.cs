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
using Gateway.Infrastructure.Services;

namespace Gateway.Infrastructure.Repository;

public class TransactionRepository : ITransactionRepository
{
    private readonly IMongoCollection<BsonDocument> _collection;
    private readonly JsonManipulationService _jsonService;

    private const string ErrorDataConvert = "Erro ao converter os dados da transação!";

    public TransactionRepository(Collections collection, JsonManipulationService jsonService)
    {
        _collection = collection.GetDataCollection();
        _jsonService = jsonService; 
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
            ObjectId objectId = ObjectId.Parse(ID);
            using(var cursor = await _collection.FindAsync(new BsonDocument { { "_id", objectId } }))
            {
                var documents = await cursor.ToListAsync();
                var data = documents.First().ToJson();
                await Task.Delay(10);
                return await Task.FromResult(_jsonService.ConvertForObject(data));
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public Task<List<JsonObject>> GetTransactions()
    {
        try
        {
            var documents = _collection.Find(Builders<BsonDocument>.Filter.Empty);
            var transaction = new List<JsonObject>();
            foreach(var document in documents.ToList())
            {
                JsonObject data = _jsonService.ConvertForObject(document.ToJson());
                transaction.Add(data);
            }
            return Task.FromResult(transaction);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Erro???????????????????????");
            throw ex;
        }
    }

    public async Task<bool> UpdateTransaction(TransactionEntity transaction, string transactionID)
    {
        try
        {
            Console.WriteLine(transaction.DataJson.ToString());
            var dataParseSucess = BsonDocument.TryParse(transaction.DataJson.ToString(), out BsonDocument data);
            if(dataParseSucess)
            {
                var objectID = ObjectId.Parse(transactionID);
                var filter = Builders<BsonDocument>.Filter.Eq("_id", objectID);
                var updateDefinition = Builders<BsonDocument>.Update.Combine(
                    data.Elements.Select(e => Builders<BsonDocument>.Update.Set(e.Name, e.Value))
        );
                await _collection.UpdateOneAsync(filter, updateDefinition   );
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
