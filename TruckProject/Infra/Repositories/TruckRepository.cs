using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;

namespace TruckProject.Infra.Repositories
{
    public class TruckRepository : IRepository<Truck>
    {
        public MongoContext Context { get; }

        public TruckRepository(MongoContext context)
        {
            Context = context;
        }

        public async Task<Truck> AddAsync(Truck truck)
        {
            await Context.GetCollection<Truck>("trucks").InsertOneAsync(truck);
            return truck;
        }

        public ReplaceOneResult Update(Expression<Func<Truck, bool>> filter, Truck truck)
        {
            return Context.GetCollection<Truck>("trucks").ReplaceOne(filter, truck);
        }

        public DeleteResult Remove(Expression<Func<Truck, bool>> filter)
        {
            return Context.GetCollection<Truck>("trucks").DeleteOne(filter);
        }

        public List<Truck> GetAll()
        {
            return Context.GetCollection<Truck>("trucks").Aggregate().ToList();
        }

        public Truck GetById(Guid id)
        {
            var queryResut = Context.GetCollection<Truck>("trucks").Find(f => f.Id == id);

            return queryResut.FirstOrDefault();
        }
    }
}
