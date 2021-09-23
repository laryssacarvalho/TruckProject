using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Entities.Enums;
using TruckProject.Domain.Services;

namespace TruckProject.Domain.CommandHandlers
{
    public class UpdateTruckCommandHandler : IRequestHandler<UpdateTruckCommand, Truck>
    {
        private readonly IService<Truck> _truckService;

        public UpdateTruckCommandHandler(IService<Truck> truckService)
        {
            _truckService = truckService;
        }

        public Task<Truck> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = _truckService.Get(request.Id);
            Enum.TryParse(request.Type, out TruckType type);

            truck.LicensePlate = request.LicensePlate;
            truck.Type = type;
            truck.Capacity = request.Capacity;

            _truckService.Update(truck);

            return Task.FromResult(truck);
        }
    }
}
