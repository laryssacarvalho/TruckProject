using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TruckProject
{
    public abstract class BaseFunction
    {
        protected readonly IMediator _mediator;
        
        protected BaseFunction(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task<IActionResult> ExecuteAsync(Func<Task<IActionResult>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                var response = new { Message = ex.Message, Error = true };

                return new JsonResult(response);
            }

        }
    }
}
