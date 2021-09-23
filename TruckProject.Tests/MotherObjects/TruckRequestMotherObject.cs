using TruckProject.Domain.Requests;

namespace TruckProject.Tests.MotherObjects
{
    class TruckRequestMotherObject
    {
        public static TruckRequest ValidTruckRequest()
        {
            return new TruckRequest
            {
                Capacity = 100,
                LicensePlate = "AAA-1333",
                Type = "VUC"
            };
        }

        public static TruckRequest InvalidTruckRequest()
        {
            return new TruckRequest
            {
                Capacity = 100,
                LicensePlate = null,
                Type = "VUC"
            };
        }
    }
}
