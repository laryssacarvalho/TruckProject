using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Requests;
using TruckProject.Tests.MotherObjects;
using Xunit;

namespace TruckProject.Tests.Functions
{
    public class CreateTruckFunctionTest
    {
        [Fact]
        public async Task Create_truck_function_executed_as_success()
        {
            var request = new TruckRequest() { LicensePlate = "123456", Capacity = 12, Type = "VUC"};
            
            var mediatorMock = GetMediatorMock();

            var function = new CreateTruckFunction(mediatorMock.Object);
            
            var response = await function.Run(request, CancellationToken.None);

            mediatorMock.Verify(x => x.Send(It.IsAny<CreateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<JsonResult>(response);
            //Assert.Equal(TruckMotherObject.ValidTruck().Capacity, response.Capacity);
            //Assert.Equal(TruckMotherObject.ValidTruck().LicensePlate, response.LicensePlate);
            //Assert.Equal(TruckMotherObject.ValidTruck().Type, response.Type);
        }

        [Fact]
        public async Task Create_truck_function_executed_as_error_when_truck_type_is_invalid()
        {
            var request = new TruckRequest() { LicensePlate = "123456", Capacity = 12, Type = "teste" };

            var mediatorMock = new Mock<IMediator>();

            var function = new CreateTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, CancellationToken.None);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(400, ((StatusCodeResult)response).StatusCode);
        }

        private Mock<IMediator> GetMediatorMock()
        {
            var mock = new Mock<IMediator>();
            
            var result = new JsonResult(TruckMotherObject.ValidTruck());

            //mock.Setup(x => x.Send(It.IsAny<CreateTruckCommand>(), CancellationToken.None))
            //    .ReturnsAsync(result);
            
            //mock.Setup(x => x.Send(It.IsAny<CreateTruckCommand>(), CancellationToken.None))
            //    .ReturnsAsync(TruckMotherObject.ValidTruck());

            //mediatorMock.Setup(x => x.Send(It.IsAny<FleetAvailabilityEmailReportCommand>(), CancellationToken.None))
            //    .ReturnsAsync(resultFun)

            return mock;
        }
    }
}
