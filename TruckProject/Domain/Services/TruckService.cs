using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TruckProject.Domain.Entities;
using TruckProject.Infra.Repositories;

namespace TruckProject.Domain.Services
{
    public class TruckService: ITruckService
    {
        private IRepository<Truck> _repository;
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

        public Truck Create(Truck truck)
        {
            _repository.Add(truck);
            return truck;
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
