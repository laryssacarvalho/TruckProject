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

        public async Task<Truck> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = await _truckService.GetAsync(request.Id);

            truck.LicensePlate = request.LicensePlate;
            truck.Type = request.Type;
            truck.Capacity = request.Capacity;

            return await _truckService.UpdateAsync(truck, cancellationToken);
        }
    }
}
