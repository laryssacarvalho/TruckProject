using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Services;

namespace TruckProject.Domain.CommandHandlers
{
    public class GetTruckCommandHandler : IRequestHandler<GetTruckCommand, List<Truck>>
    {
        private readonly IService<Truck> _truckService;

        public GetTruckCommandHandler(IService<Truck> truckService)
        {
            _truckService = truckService;
        }

        public Task<List<Truck>> Handle(GetTruckCommand request, CancellationToken cancellationToken)
        {
            List<Truck> trucks = new List<Truck>();

            if (request.Id != default)
            {
                var truck = _truckService.Get(request.Id);
                if(truck != null)
                {
                    trucks.Add(truck);
                }
            }
            else
            {
                trucks.AddRange(_truckService.GetAll());
            }
            return Task.FromResult(trucks);
        }
    }
}
