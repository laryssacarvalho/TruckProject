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
    public class DeleteTruckCommandHandler : IRequestHandler<DeleteTruckCommand, bool>
    {
        private readonly IService<Truck> _truckService;

        public DeleteTruckCommandHandler(IService<Truck> truckService)
        {
            _truckService = truckService;
        }

        public async Task<bool> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
            await _truckService.DeleteAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
