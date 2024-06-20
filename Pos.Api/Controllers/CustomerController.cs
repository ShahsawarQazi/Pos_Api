using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Contracts.Request.Customer;
using Pos.Application.Contracts.Response.Customer;
using Pos.Application.Features.Customer.Command;
using Pos.Application.Features.Customer.Queries;
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

        [HttpGet]
        [ProducesResponseType(typeof(GetCustomerResponse), StatusCodes.Status200OK)]
        [Route("Get")]
        public async Task<IActionResult> CreateCustomer(int id)
        {
            var response = await _mediator.Send(new GetCustomerCommand(id));
            return Ok(response);
        }
        [HttpPost]
        [Route("UploadCsv")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadCsv([FromForm] UploadCsvRequest uploadCsvRequest)
        {
            if (uploadCsvRequest.CsvFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var command = new UploadCsvCommand(uploadCsvRequest.CsvFile);
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("CSV uploaded successfully.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading the CSV.");
            }
        }
    }
}

