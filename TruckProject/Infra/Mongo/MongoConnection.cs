using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using TruckProject.Infra.Mongo.Interfaces;

namespace TruckProject.Infra
{
    public class MongoConnection : IMongoConnection
    {
        private static bool _isRegistered;
        private static IMongoDatabase Database;
        private readonly string _connectionString;
        private readonly string _defaultDatabaseName;

        public MongoConnection()
        {
            _connectionString = Environment.GetEnvironmentVariable("MongoConnectionString");
            _defaultDatabaseName = Environment.GetEnvironmentVariable("MongoDatabaseName");
        }

        public IMongoDatabase GetDatabase()
        {
            if (_connectionString == null)            
                throw new Exception($"Mongo connection string  is null.");

            if (Database != null)
                return Database;
            
            var urlBuilder = GetUrlBuilder(_connectionString);

            var databaseName = urlBuilder.DatabaseName;

            if (databaseName == null)
                databaseName = _defaultDatabaseName;

            var client = GetClient(urlBuilder.ToMongoUrl());
            var database = client.GetDatabase(databaseName);

            Register();
            Database = database;
            return Database;
        }

        private MongoUrlBuilder GetUrlBuilder(string connectionString) => new MongoUrlBuilder(connectionString);

        private MongoClient GetClient(MongoUrl mongoUrl) => new MongoClient(mongoUrl);

        private static void Register()
        {
            if (!_isRegistered)
            {
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
                _isRegistered = true;
            }
        }
    }
}
