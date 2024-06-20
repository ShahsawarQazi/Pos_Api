using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Common;
using Pos.Application.Common.Interfaces;
using Pos.Application.Contracts.Request.Customer;
using Pos.Application.Contracts.Response.Customer;
using Pos.Application.Features.Customer.Command;
using Pos.Application.Features.Customer.Queries;
using PosApi.Extensions.Swagger;
using System.Text;

namespace PosApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(ApiConstants.V1)]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomerExportService _customerExportService;

        public CustomerController(IMediator mediator, ICustomerExportService customerExportService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _customerExportService = customerExportService;
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

        [HttpGet]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [Route("ExportCsv")]
        public async Task<IActionResult> ExportCustomersCsv()
        {
            var Csv = Pos.Application.Common.Extensions.StringExtensions.EscapeForCsv;
            IEnumerable<CustomerExportDto> customers = await _customerExportService.GetCustomersForExportAsync();

            // Create CSV content
            var sb = new StringBuilder();
            sb.AppendLine("Name,PhoneNo,Email,CompanyName,Address");

            foreach (var customer in customers)
            {
                sb.AppendLine($"{Csv(customer.Name)},{customer.PhoneNo},{Csv(customer.Email)},{Csv(customer.CompanyName)},{Csv(customer.Address)}");
            }

            byte[] csvBytes = Encoding.UTF8.GetBytes(sb.ToString());

            // Return CSV as file
            return File(csvBytes, "text/csv", "customers.csv");
        }

       
    }
}

