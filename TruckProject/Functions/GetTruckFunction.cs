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
    public class GetTruckFunction : BaseFunction
    {
        public GetTruckFunction(IMediator mediator) : base(mediator)
        {

        }

        [FunctionName(nameof(GetTruckFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "truck/{truckId?}")] HttpRequest request, string? truckId, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                var result = await _mediator.Send(GetTruckCommand.Create(truckId != null ? Guid.Parse(truckId) : default), cancellationToken);

                if (truckId != null && result.Count == 0)
                    return new StatusCodeResult(404);

                return new JsonResult(result);
            });
        }
    }
}
