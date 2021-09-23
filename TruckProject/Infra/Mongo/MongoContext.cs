using MongoDB.Driver;
using TruckProject.Infra.Mongo.Interfaces;

namespace TruckProject.Infra
{
    public class MongoContext : IMongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IMongoConnection mongoConnection)
        {
            Database = mongoConnection.GetDatabase();
        }
        
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }
    }
}
