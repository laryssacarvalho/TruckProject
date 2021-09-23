using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

            var addedTruc = await truckService.CreateAsync(TruckMotherObject.ValidTruck(), CancellationToken.None);

            repositoryMock.Verify(x => x.AddAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(addedTruc);
            Assert.Equal(TruckMotherObject.ValidTruck().Capacity, addedTruc.Capacity);
            Assert.Equal(TruckMotherObject.ValidTruck().LicensePlate, addedTruc.LicensePlate);
            Assert.Equal(TruckMotherObject.ValidTruck().Type, addedTruc.Type);
        }

        [Fact]
        public async Task TruckService_must_throw_an_exception_when_capacity_is_invalid()
        {
            var repositoryMock = GetTruckRepositoryMock();

            var truckService = new TruckService(repositoryMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => truckService.CreateAsync(TruckMotherObject.InvalidTruckCapacityLessThanZero(), CancellationToken.None));

            repositoryMock.Verify(x => x.AddAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TruckService_must_throw_an_exception_when_license_plate_is_invalid()
        {
            var repositoryMock = GetTruckRepositoryMock();

            var truckService = new TruckService(repositoryMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => truckService.CreateAsync(TruckMotherObject.InvalidTruckLicencePlateNull(), CancellationToken.None));

            repositoryMock.Verify(x => x.AddAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task TruckService_must_get_truck_by_id_as_success()
        {
            var repositoryMock = GetTruckRepositoryMock();
            var id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d");

            var truckService = new TruckService(repositoryMock.Object);

            var truck = await truckService.CreateAsync(TruckMotherObject.ValidTruck(), CancellationToken.None);

            repositoryMock.Verify(x => x.AddAsync(It.IsAny<Truck>(), It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(truck);
            Assert.Equal(TruckMotherObject.ValidTruck().Capacity, truck.Capacity);
            Assert.Equal(TruckMotherObject.ValidTruck().LicensePlate, truck.LicensePlate);
            Assert.Equal(TruckMotherObject.ValidTruck().Type, truck.Type);
        }

        [Fact]
        public async Task TruckService_must_get_all_truck_by_as_success()
        {
            var repositoryMock = GetTruckRepositoryMock();

            var truckService = new TruckService(repositoryMock.Object);

            var trucks = truckService.GetAll();

            repositoryMock.Verify(x => x.GetAll(), Times.Once);

            Assert.NotNull(trucks);
            Assert.IsType<List<Truck>>(trucks);
            Assert.Equal(2, trucks.Count);
        }

        [Fact]
        public async Task TruckService_must_delete_truck_as_success()
        {
            var repositoryMock = GetTruckRepositoryMock();

            var truckService = new TruckService(repositoryMock.Object);
            var id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d");

            await truckService.DeleteAsync(id, CancellationToken.None);

            repositoryMock.Verify(x => x.RemoveAsync(It.IsAny<Expression<Func<Truck, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task TruckService_must_update_truck_as_success()
        {
            var repositoryMock = GetTruckRepositoryMock();

            var truckService = new TruckService(repositoryMock.Object);
            var id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d");

            await truckService.UpdateAsync(TruckMotherObject.ValidTruck(), CancellationToken.None);

            repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Expression<Func<Truck, bool>>>(), TruckMotherObject.ValidTruck(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private Mock<IRepository<Truck>> GetTruckRepositoryMock()
        {
            var mock = new Mock<IRepository<Truck>>();

            mock
                .Setup(x => x.AddAsync(It.Is<Truck>(x => x.Capacity == TruckMotherObject.ValidTruck().Capacity), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TruckMotherObject.ValidTruck());

            mock
                .Setup(x => x.GetByIdAsync(It.Is<Guid>(x => x == TruckMotherObject.ValidTruck().Id)))
                .ReturnsAsync(TruckMotherObject.ValidTruck());

            mock
                .Setup(x => x.GetAll())
                .Returns(TruckMotherObject.ValidListTruck());

            mock
                .Setup(x => x.RemoveAsync(It.IsAny<Expression<Func<Truck, bool>>>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            mock
                .Setup(x => x.UpdateAsync(It.IsAny<Expression<Func<Truck, bool>>>(), It.Is<Truck>(x => x.Id == TruckMotherObject.ValidTruck().Id), It.IsAny<CancellationToken>()))
                .Verifiable();

            return mock;
        }
    }
}
