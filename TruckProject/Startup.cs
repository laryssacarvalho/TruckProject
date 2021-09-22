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
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(builder.GetContext().ApplicationRootPath)
            //    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            //    .AddEnvironmentVariables()
            //    .Build();

            //builder.Services.Configure(configuration["MongoConnectionString"]);

            builder.Services.AddSingleton<IMongoConnection, MongoConnection>();
            builder.Services.AddSingleton<MongoContext>();

            builder.Services.AddScoped<ITruckService, TruckService>();            
            builder.Services.AddSingleton<IRepository<Truck>, TruckRepository>();
            builder.Services.AddMediatR(GetType().Assembly);

        }
    }
}
