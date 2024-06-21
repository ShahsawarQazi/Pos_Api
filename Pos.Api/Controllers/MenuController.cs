using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Contracts.Request.Customer;
using Pos.Application.Contracts.Request.Menu;
using Pos.Application.Contracts.Response.Menu;
using Pos.Application.Features.Customer.Command;
using PosApi.Extensions.Swagger;

namespace PosApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(ApiConstants.V1)]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateMenuResponse), StatusCodes.Status200OK)]
        [Route("CreateMenu")]
        public async Task<IActionResult> CreateMenu(CreateMenuRequest createMenuRequest)
        {
            var response = await _mediator.Send(new CreateMenuCommand(createMenuRequest));
            return Ok(response);
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(GetCustomerResponse), StatusCodes.Status200OK)]
        //[Route("Get")]
        //public async Task<IActionResult> GetCustomer(string name)
        //{
        //    var response = await _mediator.Send(new GetCustomerCommand(name));
        //    return Ok(response);
        //}
        
        [HttpPost]
        [Route("UploadMenuCsv")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadMenuCsv([FromForm] UploadCsvRequest uploadCsvMenuRequest)
        {
            if (uploadCsvMenuRequest.CsvFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var command = new UploadCsvCommand(uploadCsvMenuRequest.CsvFile);
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("CSV uploaded successfully.");
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while uploading the CSV.");
        }


    }
}

