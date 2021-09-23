using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Entities.Enums;
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

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            Expression<Func<Truck, bool>> filter = x => x.Id.Equals(id);
            await _repository.RemoveAsync(filter, cancellationToken);
        }

        public async Task<Truck> UpdateAsync(Truck truck, CancellationToken cancellationToken)
        {
            Validate(truck);
            Expression<Func<Truck, bool>> filter = x => x.Id.Equals(truck.Id);
            await _repository.UpdateAsync(filter, truck, cancellationToken);
            return truck;
        }

        public async Task<Truck> CreateAsync(Truck truck, CancellationToken cancellationToken)
        {
            Validate(truck);
            return await _repository.AddAsync(truck, cancellationToken);
        }

        private void Validate(Truck truck)
        {
            if(truck.Capacity == null || truck.Capacity < 0)
            {
                throw new ArgumentException("Truck capacity can't be null or less than zero");
            }

            if (truck.LicensePlate== null)
            {
                throw new ArgumentNullException("Truck license plate can't be null");
            }

            if (truck.Type == null)
            {
                throw new ArgumentNullException("Truck type can't be null");
            }

            if (!(Enum.IsDefined(typeof(TruckType), truck.Type)))
            {
                throw new ArgumentException("Invalid truck type");
            }

        }

        public async Task<Truck> GetAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
        

        public List<Truck> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
