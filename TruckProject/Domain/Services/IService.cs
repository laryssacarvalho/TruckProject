using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;

namespace TruckProject.Domain.Services
{
    public interface IService<T>
    {
        public void Delete(Guid id);

        public T Update(T entity);

        public Task<T> CreateAsync(T entity);

        public T Get(Guid id);

        public List<T> GetAll();
    }
}
