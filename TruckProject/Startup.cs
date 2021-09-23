using System;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TruckProject;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Services;
using TruckProject.Infra;
using TruckProject.Infra.Mongo.Interfaces;
using TruckProject.Infra.Repositories;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TruckProject
{
    [ExcludeFromCodeCoverage]
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IMongoConnection, MongoConnection>();
            builder.Services.AddSingleton<IMongoContext, MongoContext>();

            builder.Services.AddScoped<IService<Truck>, TruckService>();
            builder.Services.AddScoped<IRepository<Truck>, TruckRepository>();
            builder.Services.AddMediatR(GetType().Assembly);
        }
    }
}
