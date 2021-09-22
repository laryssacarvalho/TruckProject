using TruckProject.Domain.Entities;

namespace TruckProject.Tests.MotherObjects
{
    public static class TruckMotherObject
    {
        public static Truck ValidTruck()
        {
            return new Truck
            {
                Capacity = 100,
                LicensePlate = "AAA-1333",
                Type = "VUC"
            };
        }

        public static Truck InValidTruckLicencePlateNull()
        {
            return new Truck
            {
                Capacity = -15,
                LicensePlate = null,
                Type = "VUC"
            };
        }
    }
}
