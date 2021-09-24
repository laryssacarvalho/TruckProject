using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Tests.MotherObjects;
using Xunit;

namespace TruckProject.Tests.Functions
{
    public class GetTruckFunctionTest
    {
        [Fact]
        public async Task Get_all_truck_function_executed_as_success()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
           
            var mediatorMock = GetMediatorMock();

            var function = new GetTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, null, CancellationToken.None);

            Assert.IsType<JsonResult>(response);
        }

        [Fact]
        public async Task Get_truck_by_id_function_executed_as_error_when_id_is_invalid()
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());

            var mediatorMock = GetMediatorMock();

            var function = new GetTruckFunction(mediatorMock.Object);

            var response = await function.Run(request, TruckMotherObject.InvalidTruckLicencePlateNull().Id.ToString(), CancellationToken.None);

            Assert.IsType<StatusCodeResult>(response);
            Assert.Equal(404, ((StatusCodeResult)response).StatusCode);
        }

        private Mock<IMediator> GetMediatorMock()
        {
            var mock = new Mock<IMediator>();

            mock.Setup(x => x.Send(It.Is<GetTruckCommand>(x => x.Id == TruckMotherObject.ValidTruck().Id), CancellationToken.None))
                .ReturnsAsync(TruckMotherObject.ValidListTruck());

            mock.Setup(x => x.Send(It.Is<GetTruckCommand>(x => x.Id != TruckMotherObject.ValidTruck().Id), CancellationToken.None))
                .ReturnsAsync(new List<Truck>());

            return mock;
        }
    }
}
