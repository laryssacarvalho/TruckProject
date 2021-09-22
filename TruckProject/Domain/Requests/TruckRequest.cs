using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Domain.Entities.Enums;

namespace TruckProject.Domain.Requests
{
    public class TruckRequest
    {
        public string LicensePlate { get; set; }
        public string Type { get; set; }

        public double Capacity { get; set; }
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(LicensePlate) && !(Capacity == default) && (Enum.IsDefined(typeof(TruckType), Type));
        }
    }
}
