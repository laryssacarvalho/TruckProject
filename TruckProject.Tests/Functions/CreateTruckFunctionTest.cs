using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Tests.MotherObjects;
using Xunit;

namespace TruckProject.Tests.Functions
{
    public class CreateTruckFunctionTest
    {
        [Fact]
        public async Task Create_truck_function_executed_as_success()
        {
            var mediatorMock = GetMediatorMock();

            var function = new CreateTruckFunction(mediatorMock.Object);
            
            var response = await function.Run(TruckRequestMotherObject.ValidTruckRequest(), CancellationToken.None);
            var addedTruck = ((JsonResult)response).Value as Truck;

            mediatorMock.Verify(x => x.Send(It.IsAny<CreateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<JsonResult>(response);
            Assert.Equal(TruckMotherObject.ValidTruck().Capacity, addedTruck.Capacity);
            Assert.Equal(TruckMotherObject.ValidTruck().LicensePlate, addedTruck.LicensePlate);
            Assert.Equal(TruckMotherObject.ValidTruck().Type, addedTruck.Type);
        }

        [Fact]
        public async Task Create_truck_function_executed_as_error_when_truck_type_is_invalid()
        {
            var mediatorMock = GetMediatorMock();

            var function = new CreateTruckFunction(mediatorMock.Object);

            var response = await function.Run(TruckRequestMotherObject.InvalidTruckRequest(), CancellationToken.None);

            dynamic exception = ((JsonResult)response).Value;

            mediatorMock.Verify(x => x.Send(It.IsAny<CreateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<JsonResult>(response);
            Assert.Equal(true, exception?.GetType().GetProperty("Error")?.GetValue(exception, null));
        }

        private Mock<IMediator> GetMediatorMock()
        {
            var mock = new Mock<IMediator>();

            mock.Setup(x => x.Send(It.Is<CreateTruckCommand>(x => x.LicensePlate == TruckRequestMotherObject.ValidTruckRequest().LicensePlate), CancellationToken.None))
                .ReturnsAsync(TruckMotherObject.ValidTruck());

            mock.Setup(x => x.Send(It.Is<CreateTruckCommand>(x => x.LicensePlate != TruckRequestMotherObject.ValidTruckRequest().LicensePlate), CancellationToken.None))
                .Throws(new ArgumentNullException());

            return mock;
        }
    }
}
