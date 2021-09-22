using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TruckProject.Domain.Entities;
using TruckProject.Domain.Requests;

namespace TruckProject.Domain.Commands
{
    public class DeleteTruckCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public static DeleteTruckCommand Create(Guid id) => new DeleteTruckCommand { Id = id };
    }
}
