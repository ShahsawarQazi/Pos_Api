using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Contracts.Request.Category;
using Pos.Application.Contracts.Response.Category;
using Pos.Application.Features.Category.Command;
using Pos.Application.Features.Category.Queries;
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
        [Route("CreateParentCategory")]
        public async Task<IActionResult> CreateParentCategory(CreateParentCategoryRequest createParentCategoryRequest)
        {
            var response = await _mediator.Send(new CreateParentCategoryCommand(createParentCategoryRequest));
            return response ? Ok("Parent category created successfully.") : StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the parent category.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetParentCategoryResponse), StatusCodes.Status200OK)]
        [Route("GetParentCategory")]
        public async Task<IActionResult> GetParentCategory(string name)
        {
            var response = await _mediator.Send(new GetParentCategoryQuery(name));
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("UpdateParentCategory")]
        public async Task<IActionResult> UpdateParentCategory( UpdateParentCategoryRequest request)
        {
            var result = await _mediator.Send(new UpdateParentCategoryCommand(request));

            if (result)
            {
                return Ok("Parent category updated successfully.");
            }

            return NotFound("Parent category not found.");
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Route("CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory(CreateSubCategoryRequest createSubCategoryRequest)
        {
            var response = await _mediator.Send(new CreateSubCategoryCommand(createSubCategoryRequest));
            return response ? Ok("Sub category created successfully.") : StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the Sub category.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetSubCategoryResponse), StatusCodes.Status200OK)]
        [Route("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory(string name)
        {
            var response = await _mediator.Send(new GetSubCategoryQuery(name));
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("UpdateSubCategory")]
        public async Task<IActionResult> UpdateSubCategory(UpdateSubCategoryRequest request)
        {
            var result = await _mediator.Send(new UpdateSubCategoryCommand(request));

            if (result)
            {
                return Ok("Sub category updated successfully.");
            }

            return NotFound("Sub category not found.");
        }
    }
}
