using System;
using System.Threading.Tasks;
using Moq;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Services;
using TruckProject.Infra.Repositories;
using TruckProject.Tests.MotherObjects;
using Xunit;

namespace TruckProject.Tests.Services
{
    public class TruckServiceTest
    {
        [Fact]
        public async Task TruckService_must_add_a_truck_as_success()
        {
            var repositoryMock = GetTruckRepositoryMock();

            var truckService = new TruckService(repositoryMock.Object);

            var addedTruc = await truckService.CreateAsync(TruckMotherObject.ValidTruck());

            repositoryMock.Verify(x => x.AddAsync(It.IsAny<Truck>()), Times.Once);

            Assert.NotNull(addedTruc);
            Assert.Equal(TruckMotherObject.ValidTruck().Capacity, addedTruc.Capacity);
            Assert.Equal(TruckMotherObject.ValidTruck().LicensePlate, addedTruc.LicensePlate);
            Assert.Equal(TruckMotherObject.ValidTruck().Type, addedTruc.Type);
        }


        [Fact]
        public async Task TruckService_must_throw_an_exception_when_capacity_is_invalid()
        {
            //TODO
        }


        private Mock<IRepository<Truck>> GetTruckRepositoryMock()
        {
            var mock = new Mock<IRepository<Truck>>();

            mock
                .Setup(x => x.AddAsync(It.Is<Truck>(x => x.Capacity == TruckMotherObject.ValidTruck().Capacity)))
                .ReturnsAsync(TruckMotherObject.ValidTruck());

            return mock;
        }

       
    }
}
