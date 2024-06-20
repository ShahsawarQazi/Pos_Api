using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Contracts.Request.Category;
using Pos.Application.Contracts.Response.Category;
using Pos.Application.Contracts.Response.Customer;
using Pos.Application.Features.Category.Command;
using Pos.Application.Features.Category.Queries;
using Pos.Application.Features.Customer.Queries;
using PosApi.Extensions.Swagger;

namespace PosApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(ApiConstants.V1)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Route("Create")]
        public async Task<IActionResult> CreateParentCategory(CreateParentCategoryRequest createParentCategoryRequest)
        {
            var response = await _mediator.Send(new CreateParentCategoryCommand(createParentCategoryRequest));
            return response ? Ok("Parent category created successfully.") : StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the parent category.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetParentCategoryResponse), StatusCodes.Status200OK)]
        [Route("Get")]
        public async Task<IActionResult> GetCategory(string name)
        {
            var response = await _mediator.Send(new GetParentCategoryQuery(name));
            return Ok(response);
        }
    }
}
