using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TruckProject.Infra.Repositories
{
    public interface IRepository<T>
    {
        MongoContext Context { get; }

        public Task<T> AddAsync(T entity);

        public ReplaceOneResult Update(Expression<Func<T, bool>> filter, T entity);

        public DeleteResult Remove(Expression<Func<T, bool>> filter);

        public List<T> GetAll();

        public T GetById(Guid id);
    }
}
