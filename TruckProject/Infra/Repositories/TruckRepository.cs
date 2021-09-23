using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;
using TruckProject.Infra.Mongo.Interfaces;

namespace TruckProject.Infra.Repositories
{
    public class TruckRepository : IRepository<Truck>
    {
        private readonly IMongoContext _context;

        public TruckRepository(IMongoContext context)
        {
            _context = context;
        }

        public async Task<Truck> AddAsync(Truck truck)
        {
            await _context.GetCollection<Truck>("trucks").InsertOneAsync(truck);
            return truck;
        }

        public ReplaceOneResult Update(Expression<Func<Truck, bool>> filter, Truck truck)
        {
            return _context.GetCollection<Truck>("trucks").ReplaceOne(filter, truck);
        }

        public DeleteResult Remove(Expression<Func<Truck, bool>> filter)
        {
            return _context.GetCollection<Truck>("trucks").DeleteOne(filter);
        }

        public List<Truck> GetAll()
        {
            return _context.GetCollection<Truck>("trucks").Aggregate().ToList();
        }

        public Truck GetById(Guid id)
        {
            var queryResut = _context.GetCollection<Truck>("trucks").Find(f => f.Id == id);

            return queryResut.FirstOrDefault();
        }
    }
}
