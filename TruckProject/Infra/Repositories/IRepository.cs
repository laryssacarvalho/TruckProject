using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TruckProject.Infra.Repositories
{
    public interface IRepository<T>
    {
        public Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        public Task UpdateAsync(Expression<Func<T, bool>> filter, T entity, CancellationToken cancellationToken);

        public Task RemoveAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);

        public Task<List<T>> GetAllAsync();

        public Task<T> GetByIdAsync(Guid id);
    }
}
