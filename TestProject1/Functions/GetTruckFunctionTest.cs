using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Xunit;
using TruckProject.Domain.Commands;
using TruckProject;
using TruckProject.Domain.Requests;
using TruckProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace TestProject1
{
    public class GetTruckFunctionTest
    {
        [Fact]
        public async Task Get_all_truck_function_executed_as_success()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
           
            var mediatorMock = new Mock<IMediator>();

            var function = new GetTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, null, CancellationToken.None);

            Assert.IsType<JsonResult>(response);
        }

        [Fact]
        public async Task Get_truck_by_id_function_executed_as_error_when_id_is_invalid()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());

            var mediatorMock = new Mock<IMediator>();

            var function = new GetTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, "00000000-0000-0000-0000-000000000000", CancellationToken.None);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(404, ((StatusCodeResult)response).StatusCode);
        }
    }
}
