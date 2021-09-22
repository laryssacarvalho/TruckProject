using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Infra.Mongo;
using TruckProject.Infra.Mongo.Interfaces;

namespace TruckProject.Infra
{
    public class MongoContext : ContextBase
    {
        public MongoContext(IMongoConnection mongoConnection) : base(mongoConnection)
        {

        }
    }
}
