using MongoDB.Driver;

namespace TruckProject.Infra.Mongo.Interfaces
{
    public interface IMongoConnection
    {
        IMongoDatabase GetDatabase();

    }
}
