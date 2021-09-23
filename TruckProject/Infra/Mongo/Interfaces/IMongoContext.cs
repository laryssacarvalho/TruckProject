using MongoDB.Driver;

namespace TruckProject.Infra.Mongo.Interfaces
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
        public IMongoCollection<T> GetCollection<T>(string name);
    }
}
