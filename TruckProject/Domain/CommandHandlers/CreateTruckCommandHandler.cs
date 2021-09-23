using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Entities.Enums;
using TruckProject.Domain.Services;

namespace TruckProject.Domain.CommandHandlers
{
    public class CreateTruckCommandHandler : IRequestHandler<CreateTruckCommand, Truck>
    {
        private readonly IService<Truck> _truckService;

        public CreateTruckCommandHandler(IService<Truck> truckService)
        {
            _truckService = truckService;
        }

        public async Task<Truck> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {            
            var truck = new Truck()
            {
                Capacity = request.Capacity,
                LicensePlate = request.LicensePlate,
                Type = request.Type
            };

            var result = await _truckService.CreateAsync(truck, cancellationToken);

            return result;
        }
    }
}
