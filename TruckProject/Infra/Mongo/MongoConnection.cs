using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Domain.Settings;
using TruckProject.Infra.Mongo.Interfaces;

namespace TruckProject.Infra
{
    public class MongoConnection : IMongoConnection
    {
        private static bool _isRegistered;
        private static readonly IMongoDatabase Database;
        private readonly string _connectionString;
        private readonly string _defaultDatabaseName;

        public MongoConnection(IOptions<ApplicationSettings> options)
        {
            _connectionString = options.Value.MongoConnectionString;
            _defaultDatabaseName = options.Value.MongoDatabaseName;
        }

        public IMongoDatabase GetDatabase()
        {
            if (_connectionString == null)
            {
                var exception = new Exception($"Mongo connection string  is null.");
                throw exception;
            }

            //var client = new MongoClient(_connectionString);
            //var database = client.GetDatabase(_defaultDatabaseName);

            //if (database != null) return database;

            var urlBuilder = GetUrlBuilder(_connectionString);

            var databaseName = urlBuilder.DatabaseName;

            if (databaseName == null)
                databaseName = _defaultDatabaseName;

            var client = GetClient(urlBuilder.ToMongoUrl());
            var database = client.GetDatabase(databaseName);

            Register();

            return database;
        }

        private MongoUrlBuilder GetUrlBuilder(string connectionString) => new MongoUrlBuilder(connectionString);

        private MongoClient GetClient(MongoUrl mongoUrl) => new MongoClient(mongoUrl);

        private static void Register()
        {
            var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("IgnoreElements", conventionPack, type => true);

            if (!_isRegistered)
            {
                BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
                _isRegistered = true;
            }
        }

    }
}
