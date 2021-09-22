using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Requests;

namespace TruckProject.Domain.Commands
{
    public class GetTruckCommand : IRequest<List<Truck>>
    {
        public Guid Id { get; set; }

        public static GetTruckCommand Create(Guid id = default) => new GetTruckCommand { Id = id };
    }
}
