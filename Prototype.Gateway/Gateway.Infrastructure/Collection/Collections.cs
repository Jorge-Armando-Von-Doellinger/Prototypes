using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gateway.Infrastructure.Collection;

public class Collections
{
    public IMongoCollection<BsonDocument> GetDataCollection() 
    {
        try
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database =  client.GetDatabase("transactions");
            return database.GetCollection<BsonDocument>("DataTransactions");
        }
        catch (Exception ex)
        {
            throw new MongoException(ex.Message);
        }
    }
}
