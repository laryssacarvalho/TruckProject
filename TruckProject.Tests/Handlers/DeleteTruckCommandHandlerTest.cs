using Moq;
using System;
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
    public class DeleteTruckCommandHandlerTest
    {
        [Fact]
        public async Task DeleteTruckCommandHandler_must_add_a_truck_as_success()
        {
            var serviceMock = GetTruckServiceMock();

            var commandHandler = new DeleteTruckCommandHandler(serviceMock.Object);
            var id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d");

            var response = await commandHandler.Handle(DeleteTruckCommand.Create(id), CancellationToken.None);

            serviceMock.Verify(x => x.DeleteAsync(It.Is<Guid>(x => x == TruckMotherObject.ValidTruck().Id), It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsType<bool>(response);
        }

        private Mock<IService<Truck>> GetTruckServiceMock()
        {
            var mock = new Mock<IService<Truck>>();

            mock
                .Setup(x => x.DeleteAsync(It.Is<Guid>(x => x == TruckMotherObject.ValidTruck().Id), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            return mock;
        }
    }
}
