using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Tests.MotherObjects;
using Xunit;

namespace TruckProject.Tests.Functions
{
    public class UpdateTruckFunctionTest
    {
        [Fact]
        public async Task Update_truck_function_executed_as_success()
        {
            var mediatorMock = GetMediatorMock();

            var function = new UpdateTruckFunction(mediatorMock.Object);

            var response = await function.Run(TruckRequestMotherObject.ValidTruckRequest(), TruckMotherObject.ValidTruck().Id.ToString(), CancellationToken.None);
            mediatorMock.Verify(x => x.Send(It.IsAny<UpdateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(200, ((StatusCodeResult)response).StatusCode);
        }

        [Fact]
        public async Task Update_truck_function_executed_as_error_when_id_is_null()
        {
            var mediatorMock = GetMediatorMock();

            var function = new UpdateTruckFunction(mediatorMock.Object);

            var response = await function.Run(TruckRequestMotherObject.ValidTruckRequest(), null, CancellationToken.None);
            mediatorMock.Verify(x => x.Send(It.IsAny<UpdateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Never);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(400, ((StatusCodeResult)response).StatusCode);
        }

        [Fact]
        public async Task Update_truck_function_executed_as_error_when_request_is_null()
        {
            var mediatorMock = GetMediatorMock();

            var function = new UpdateTruckFunction(mediatorMock.Object);

            var response = await function.Run(null, TruckMotherObject.InvalidTruckLicencePlateNull().Id.ToString(), CancellationToken.None);
            mediatorMock.Verify(x => x.Send(It.IsAny<UpdateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Never);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(400, ((StatusCodeResult)response).StatusCode);
        }

        [Fact]
        public async Task Update_truck_function_executed_as_error_when_id_is_invalid()
        {
            var mediatorMock = GetMediatorMock();

            var function = new UpdateTruckFunction(mediatorMock.Object);

            var response = await function.Run(TruckRequestMotherObject.ValidTruckRequest(), TruckMotherObject.InvalidTruckLicencePlateNull().Id.ToString(), CancellationToken.None);
            mediatorMock.Verify(x => x.Send(It.IsAny<UpdateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Never);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(404, ((StatusCodeResult)response).StatusCode);
        }

        [Fact]
        public async Task Update_truck_function_executed_as_error_when_truck_request_is_invalid()
        {
            var mediatorMock = GetMediatorMock();

            var function = new UpdateTruckFunction(mediatorMock.Object);

            var response = await function.Run(TruckRequestMotherObject.InvalidTruckRequest(), TruckMotherObject.ValidTruck().Id.ToString(), CancellationToken.None);

            dynamic exception = ((JsonResult)response).Value;

            mediatorMock.Verify(x => x.Send(It.IsAny<UpdateTruckCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<JsonResult>(response);
            Assert.Equal(true, exception?.GetType().GetProperty("Error")?.GetValue(exception, null));
        }
        private Mock<IMediator> GetMediatorMock()
        {
            var mock = new Mock<IMediator>();

            mock.Setup(x => x.Send(It.Is<UpdateTruckCommand>(x => x.LicensePlate == TruckRequestMotherObject.ValidTruckRequest().LicensePlate), CancellationToken.None))
                .ReturnsAsync(TruckMotherObject.ValidTruck());

            mock.Setup(x => x.Send(It.Is<UpdateTruckCommand>(x => x.LicensePlate != TruckRequestMotherObject.ValidTruckRequest().LicensePlate), CancellationToken.None))
                .Throws(new ArgumentNullException());

            mock.Setup(x => x.Send(It.Is<GetTruckCommand>(x => x.Id == TruckMotherObject.ValidTruck().Id), CancellationToken.None))
                .ReturnsAsync(TruckMotherObject.ValidListTruck());

            mock.Setup(x => x.Send(It.Is<GetTruckCommand>(x => x.Id != TruckMotherObject.ValidTruck().Id), CancellationToken.None))
                .ReturnsAsync(new List<Truck>());

            return mock;
        }
    }
}
