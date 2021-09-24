using System;
using System.Collections.Generic;
using TruckProject.Domain.Entities;

namespace TruckProject.Tests.MotherObjects
{
    public static class TruckMotherObject
    {
        public static Truck ValidTruck()
        {
            return new Truck
            {
                Id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d"),
                Capacity = 100,
                LicensePlate = "AAA-1333",
                Type = "VUC"
            };
        }

        public static Truck InvalidTruckLicencePlateNull()
        {
            return new Truck
            {
                Id = new Guid("4aade100-cb8a-4bf9-874d-c0117f848b4e"),
                Capacity = 12,
                LicensePlate = null,
                Type = "Toco"
            };
        }

        public static Truck InvalidTypeTruck()
        {
            return new Truck
            {
                Capacity = 12,
                LicensePlate = "AAA-1111",
                Type = "teste"
            };
        }

        public static Truck InvalidTruckCapacityLessThanZero()
        {
            return new Truck
            {
                Capacity = -5,
                LicensePlate = "AAA-1111",
                Type = "Bitruck"
            };
        }

        public static List<Truck> ValidListTruck()
        {
            List<Truck> trucks = new List<Truck>
            {
                new Truck
                {
                    Id = new Guid("4ebb6003-03a4-4e93-9870-9d1197e0791d"),
                    Capacity = 100,
                    LicensePlate = "AAA-1111",
                    Type = "VUC"
                },
                new Truck
                {
                    Id = new Guid("7d1d9beb-88c9-4b29-b8b5-b9d6a1bc4271"),
                    Capacity = 100,
                    LicensePlate = "AAA-2222",
                    Type = "Toco"
                },

            };
            return trucks;
        }
    }
}
