using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Infra.Mongo.Interfaces;

namespace TruckProject.Infra.Mongo
{
    public abstract class ContextBase
    {
        protected ContextBase(IMongoConnection mongoConnection)
        {
            Database = mongoConnection.GetDatabase();
            Configuring();
        }

        public IMongoDatabase Database { get; }

        protected virtual void Configure()
        {
        }

        protected void Register<T>()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }

        private void Configuring()
        {
            Configure();
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return Database.GetCollection<T>(name);
        }
    }
}
