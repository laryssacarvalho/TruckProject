using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TruckProject.Tests.Functions
{
    public class DeleteTruckFunctionTest
    {
        [Fact]
        public async Task Delete_truck_function_executed_as_success()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());

            var mediatorMock = new Mock<IMediator>();

            var function = new DeleteTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, "00000000-0000-0000-0000-000000000000", CancellationToken.None);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(200, ((StatusCodeResult)response).StatusCode);
        }

        [Fact]
        public async Task Delete_truck_function_executed_as_error_when_request_is_invalid()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());

            var mediatorMock = new Mock<IMediator>();

            var function = new DeleteTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, null, CancellationToken.None);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(400, ((StatusCodeResult)response).StatusCode);
        }
    }
}
