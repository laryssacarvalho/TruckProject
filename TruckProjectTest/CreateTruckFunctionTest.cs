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

namespace TruckProjectTest
{
    public class CreateTruckFunctionTest
    {
        [Fact]
        public async Task Create_truck_function_executed_as_success()
        {            
            var request = new TruckRequest() { LicensePlate = "123456", Capacity = 12, Type = "VUC" };
            //var mediatorMock = SetupMediatorMock();
            var mediatorMock = new Mock<IMediator>();

            var function = new CreateTruckFunction(mediatorMock.Object);            
            // arrange
            var response = await function.Run(request, CancellationToken.None);

            Assert.IsType<JsonResult>(response);
            // act
            //var response = await function.Run(new TruckRequest { LicensePlate = "AAA-1234", Capacity = 100, Type = "VUC" }, CancellationToken.None);

            //mediatorMock.Verify(mock => mock.Send(It.IsAny<CreateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        //[Fact]
        //public async Task Vehicle_availability_function_executed_as_error_when_plate_is_invalid()
        //{
        //    // arrange
        //    var mediatorMock = SetupMediatorMock();
        //    ILog logger = new SeriLogger(SeriLoggerConfigurationMock.Create(Fake.DebugLogLevel), LogMessageMock.Create());
        //    var function = new VehicleAvailabilityFunction(mediatorMock.Object, logger);

        //    // act
        //    await function.Run(new VehicleAvailabilityRequest { Plate = null }, CancellationToken.None);

        //    mediatorMock
        //        .Verify(mock => mock.Send(It.IsAny<VehicleAvailabilityCommand>(), It.IsAny<CancellationToken>()), Times.Never);
        //}

        private static Mock<IMediator> SetupMediatorMock()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<CreateTruckCommand>(), CancellationToken.None)).ReturnsAsync(new Truck());

            return mediatorMock;
        }
    }
}
