using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gateway.Infrastructure.Collection;

public class Collections
{
    public IMongoCollection<BsonDocument> GetDataCollection() 
    {
        var client = new MongoClient("mongodb://mongodb:26017");
        var database =  client.GetDatabase("transactions");
        return database.GetCollection<BsonDocument>("DataTransactions");
    }
}
