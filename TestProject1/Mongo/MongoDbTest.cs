//using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.CommandHandlers;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Infra;
using TruckProject.Infra.Mongo.Interfaces;
using TruckProject.Infra.Repositories;
using Xunit;

namespace TestProject1.Mongo
{
    public class MongoDbTest
    {
        [Fact]
        public void MongoConnection_Constructor_Success()
        {
            Environment.SetEnvironmentVariable("MongoConnectionString", "mongodb+srv://root:uhJFbUfTO8nJm2Jr@cluster0.id5rw.mongodb.net/TruckDB");
            Environment.SetEnvironmentVariable("MongoDatabaseName", "TesteDB");

            //Act 
            var mock = new Mock<IMongoConnection>();
            var context = new MongoContext(mock.Object);

            //Assert 
            Assert.NotNull(context);
        }        
    }
}