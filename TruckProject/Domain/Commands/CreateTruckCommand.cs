using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Requests;

namespace TruckProject.Domain.Commands
{
    public class CreateTruckCommand : IRequest<Truck>
    {
        public string LicensePlate { get; set; }
        public string Type { get; set; }

        public double Capacity { get; set; }

        public static CreateTruckCommand Create(TruckRequest request) => new CreateTruckCommand { LicensePlate = request.LicensePlate, Type = request.Type, Capacity = request.Capacity };
    }
}
