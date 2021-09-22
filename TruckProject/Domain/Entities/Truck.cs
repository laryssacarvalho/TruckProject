using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TruckProject.Domain.Entities
{
    public class Truck
    {
        public Guid Id { get; set; }

        public double Capacity { get; set; }

        public string Type { get; set; }

        public string LicensePlate { get; set; }

        public Truck()
        {
            Id = Guid.NewGuid();
        }
    }
}
