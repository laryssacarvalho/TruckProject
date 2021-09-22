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

namespace TestProject1
{
    public class CreateTruckFunctionTest
    {
        [Fact]
        public async Task Create_truck_function_executed_as_success()
        {
            var request = new TruckRequest() { LicensePlate = "123456", Capacity = 12, Type = "VUC" };
            
            var mediatorMock = new Mock<IMediator>();

            var function = new CreateTruckFunction(mediatorMock.Object);
            
            var response = await function.Run(request, CancellationToken.None);

            Assert.IsType<JsonResult>(response);
        }

        [Fact]
        public async Task Create_truck_function_executed_as_error_when_request_is_invalid()
        {
            var request = new TruckRequest() { LicensePlate = "123456", Capacity = 12, Type = "teste" };

            var mediatorMock = new Mock<IMediator>();

            var function = new CreateTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, CancellationToken.None);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(400, ((StatusCodeResult)response).StatusCode);
        }
    }
}
