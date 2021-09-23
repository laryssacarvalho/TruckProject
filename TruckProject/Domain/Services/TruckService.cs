using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;
using TruckProject.Infra.Repositories;

namespace TruckProject.Domain.Services
{
    public class TruckService: IService<Truck>
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
            Validate(truck);
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
            if(truck.Capacity == null || truck.Capacity < 0)
            {
                throw new Exception("Truck capacity can't be null or less than zero");
            }

            if (truck.LicensePlate== null)
            {
                throw new Exception("Truck license plate can't be null");
            }

            if (truck.Type == null)
            {
                throw new Exception("Truck type can't be null");
            }
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
