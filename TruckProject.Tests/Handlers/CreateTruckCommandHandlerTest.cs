using Moq;
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
    public class CreateTruckCommandHandlerTest
    {
        [Fact]
        public async Task CreateTruckCommandHandler_must_add_a_truck_as_success()
        {
            var serviceMock = GetTruckServiceMock();

            var commandHandler = new CreateTruckCommandHandler(serviceMock.Object);            

            var response = await commandHandler.Handle(CreateTruckCommand.Create(TruckRequestMotherObject.ValidTruckRequest()), CancellationToken.None);

            serviceMock.Verify(x => x.CreateAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(response);
            Assert.IsType<Truck>(response);

            Assert.Equal(TruckRequestMotherObject.ValidTruckRequest().Capacity, response.Capacity);
            Assert.Equal(TruckRequestMotherObject.ValidTruckRequest().LicensePlate, response.LicensePlate);
            Assert.Equal(TruckRequestMotherObject.ValidTruckRequest().Type, response.Type);
        }        

        private Mock<IService<Truck>> GetTruckServiceMock()
        {
            var mock = new Mock<IService<Truck>>();

            mock
                .Setup(x => x.CreateAsync(It.Is<Truck>(x => x.Capacity == TruckMotherObject.ValidTruck().Capacity), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TruckMotherObject.ValidTruck());

            return mock;
        }
    }
}
