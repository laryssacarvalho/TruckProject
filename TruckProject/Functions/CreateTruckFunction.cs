using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Requests;

namespace TruckProject
{
    public class CreateTruckFunction : BaseFunction
    {
        public CreateTruckFunction(IMediator mediator) : base(mediator)
        {

        }

        [FunctionName(nameof(CreateTruckFunction))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "truck")] TruckRequest request, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                if (request == null || !request.IsValid())
                    return new StatusCodeResult(400);

                var result = await _mediator.Send(CreateTruckCommand.Create(request), cancellationToken);

                return new JsonResult(result);
            });
        }
    }
}
