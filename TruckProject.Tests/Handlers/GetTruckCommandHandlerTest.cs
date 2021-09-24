using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TruckProject.Domain.CommandHandlers;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Services;
using TruckProject.Tests.MotherObjects;
using Xunit;

namespace TruckProject.Tests.Handlers
{
    public class GetTruckCommandHandlerTest
    {
        [Fact]
        public async Task GetTruckCommandHandler_must_get_by_id_as_success()
        {
            var serviceMock = GetTruckServiceMock();

            var commandHandler = new GetTruckCommandHandler(serviceMock.Object);
            var id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d");

            var response = await commandHandler.Handle(GetTruckCommand.Create(id), CancellationToken.None);

            serviceMock.Verify(x => x.GetAsync(It.Is<Guid>(x => x == TruckMotherObject.ValidTruck().Id)), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<List<Truck>>(response);
            Assert.Single(response);
        }

        [Fact]
        public async Task GetTruckCommandHandler_must_get_all_as_success()
        {
            var serviceMock = GetTruckServiceMock();

            var commandHandler = new GetTruckCommandHandler(serviceMock.Object);

            var response = await commandHandler.Handle(GetTruckCommand.Create(), CancellationToken.None);

            serviceMock.Verify(x => x.GetAllAsync(), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<List<Truck>>(response);
            Assert.Equal(2, response.Count);
        }

        private Mock<IService<Truck>> GetTruckServiceMock()
        {
            var mock = new Mock<IService<Truck>>();

            mock
                .Setup(x => x.GetAsync(It.Is<Guid>(x => x == TruckMotherObject.ValidTruck().Id)))
                .ReturnsAsync(TruckMotherObject.ValidTruck());

            mock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(TruckMotherObject.ValidListTruck());

            return mock;
        }
    }
}
