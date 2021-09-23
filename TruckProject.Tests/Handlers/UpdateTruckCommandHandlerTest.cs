
using TruckProject.Domain.CommandHandlers;
using TruckProject.Domain.Commands;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Services;
using TruckProject.Tests.MotherObjects;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TruckProject.Tests.Handlers
{
    public class UpdateTruckCommandHandlerTest
    {
        [Fact]
        public async Task UpdateTruckCommandHandler_must_update_a_truck_as_success()
        {
            var serviceMock = GetTruckServiceMock();

            var commandHandler = new UpdateTruckCommandHandler(serviceMock.Object);

            var id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d");

            var response = await commandHandler.Handle(UpdateTruckCommand.Create(id, TruckRequestMotherObject.ValidTruckRequest()), CancellationToken.None);

            serviceMock.Verify(x => x.UpdateAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<Truck>(response);

            Assert.Equal(TruckMotherObject.ValidTruck().Id, response.Id);
            Assert.Equal(TruckMotherObject.ValidTruck().Capacity, response.Capacity);
            Assert.Equal(TruckMotherObject.ValidTruck().LicensePlate, response.LicensePlate);
            Assert.Equal(TruckMotherObject.ValidTruck().Type, response.Type);
        }

        private Mock<IService<Truck>> GetTruckServiceMock()
        {
            var mock = new Mock<IService<Truck>>();

            mock
                .Setup(x => x.UpdateAsync(It.Is<Truck>(x => x.Capacity == TruckMotherObject.ValidTruck().Capacity), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TruckMotherObject.ValidTruck());
            mock
               .Setup(x => x.GetAsync(It.Is<Guid>(x => x == TruckMotherObject.ValidTruck().Id)))
               .ReturnsAsync(TruckMotherObject.ValidTruck());

            return mock;
        }
    }
}

