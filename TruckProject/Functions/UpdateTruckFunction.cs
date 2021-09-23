using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Requests;

namespace TruckProject
{
    public class UpdateTruckFunction : BaseFunction
    {
        public UpdateTruckFunction(IMediator mediator) : base(mediator)
        {

        }

        [FunctionName(nameof(UpdateTruckFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "truck/{truckId}")] TruckRequest request, string truckId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                if (truckId == null)
                    return new StatusCodeResult(400);

                if (request == null)
                    return new StatusCodeResult(400);

                var truck = await _mediator.Send(GetTruckCommand.Create(Guid.Parse(truckId)), cancellationToken);
                
                if(truck == null)
                    return new StatusCodeResult(404);
                
                await _mediator.Send(UpdateTruckCommand.Create(Guid.Parse(truckId), request), cancellationToken);

                return new StatusCodeResult(200);
            });
        }
    }
}
