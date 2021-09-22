using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;

namespace TruckProject.Infra.Repositories
{
    public interface IRepository<T>
    {
        //readonly MongoContext _context;  
        MongoContext Context { get; }

        public T Add(T entity);

        public ReplaceOneResult Update(Expression<Func<T, bool>> filter, T entity);

        public DeleteResult Remove(Expression<Func<T, bool>> filter);

        public List<T> GetAll();

        public T GetById(Guid id);
    }
}
