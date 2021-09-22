using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;

namespace TruckProject
{
    public class DeleteTruckFunction : BaseFunction
    {
        public DeleteTruckFunction(IMediator mediator) : base(mediator)
        {

        }

        [FunctionName(nameof(DeleteTruckFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "truck/{truckId}")] HttpRequest request, string truckId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                if (truckId == null)
                    return new StatusCodeResult(400);

                await _mediator.Send(DeleteTruckCommand.Create(Guid.Parse(truckId)), cancellationToken);

                return new StatusCodeResult(200);
            });
        }
    }
}
