using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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

        public async Task<Truck> AddAsync(Truck truck, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.GetCollection<Truck>("trucks").InsertOneAsync(truck);
            return truck;
        }

        public async Task UpdateAsync(Expression<Func<Truck, bool>> filter, Truck truck, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.GetCollection<Truck>("trucks").ReplaceOneAsync(filter, truck);
        }

        public async Task RemoveAsync(Expression<Func<Truck, bool>> filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.GetCollection<Truck>("trucks").DeleteOneAsync(filter);
        }

        public List<Truck> GetAll()
        {
            return _context.GetCollection<Truck>("trucks").Aggregate().ToList();
        }        

        public async Task<Truck> GetByIdAsync(Guid id)
        {
            var queryResut = await _context.GetCollection<Truck>("trucks").FindAsync(f => f.Id == id);

            return queryResut.FirstOrDefault();
        }
    }
}
