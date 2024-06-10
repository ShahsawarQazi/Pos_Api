using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Contracts.Request.Customer;
using Pos.Application.Contracts.Response.Customer;
using Pos.Application.Features.Customer.Command;
using PosApi.Extensions.Swagger;

namespace PosApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(ApiConstants.V1)]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCustomerResponse), StatusCodes.Status200OK)]
        [Route("Create")]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest createCustomerRequest)
        {
            var response = await _mediator.Send(new CreateCustomerCommand(createCustomerRequest));
            return Ok(response);
        }

    }
}
