using MediatR;
using System;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Requests;

namespace TruckProject.Domain.Commands
{
    public class UpdateTruckCommand : IRequest<Truck>
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public double Capacity { get; set; }
        public static UpdateTruckCommand Create(Guid id, TruckRequest request) => new UpdateTruckCommand { Id = id, LicensePlate = request.LicensePlate, Capacity = request.Capacity, Type = request.Type };
    }
}
