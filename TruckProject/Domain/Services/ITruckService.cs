using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Domain.Entities;

namespace TruckProject.Domain.Services
{
    public interface ITruckService
    {
        public void Delete(Guid id);

        public Truck Update(Truck truck);

        public Truck Create(Truck truck);

        public Truck Get(Guid id);

        public List<Truck> GetAll();
    }
}
