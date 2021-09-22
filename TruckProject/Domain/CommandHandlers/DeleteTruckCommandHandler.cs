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
        private readonly ITruckService _truckService;

        public DeleteTruckCommandHandler(ITruckService truckService)
        {
            _truckService = truckService;
        }

        public Task<bool> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
            _truckService.Delete(request.Id);

            return Task.FromResult(true);
        }
    }
}
