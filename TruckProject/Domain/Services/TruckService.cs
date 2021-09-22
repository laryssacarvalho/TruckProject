using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;
using TruckProject.Infra.Repositories;

namespace TruckProject.Domain.Services
{
    public class TruckService: ITruckService
    {
        private readonly IRepository<Truck> _repository;
        
        public TruckService(IRepository<Truck> repository)
        {
            _repository = repository;
        }

        public void Delete(Guid id)
        {
            Expression<Func<Truck, bool>> filter = x => x.Id.Equals(id);
            _repository.Remove(filter);
        }

        public Truck Update(Truck truck)
        {
            Expression<Func<Truck, bool>> filter = x => x.Id.Equals(truck.Id);
            _repository.Update(filter, truck);
            return truck;
        }

        public async Task<Truck> CreateAsync(Truck truck)
        {
            Validate(truck);
            return await _repository.AddAsync(truck);
        }

        private void Validate(Truck truck)
        {
            if (truck.Capacity < 0)
                throw new Exception("Truck capacity can't be less than zero");
        }

        public Truck Get(Guid id)
        {
            return _repository.GetById(id);
        }

        public List<Truck> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
