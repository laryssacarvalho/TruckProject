using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;

namespace TruckProject.Domain.Services
{
    public interface IService<T>
    {
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);

        public Task<T> CreateAsync(T entity, CancellationToken cancellationToken);

        public Task<T> GetAsync(Guid id);

        public List<T> GetAll();
    }
}
