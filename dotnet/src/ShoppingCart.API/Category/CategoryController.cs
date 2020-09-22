using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Category;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.API.Category
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryReader categoryReader;
        private readonly IMediator mediator;

        public CategoryController(ICategoryReader categoryReader, IMediator mediator)
        {
            this.categoryReader = categoryReader;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(Guid id)
        {
            var category = categoryReader.GetByIDs(new[] {id}).SingleOrDefault();
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var categories = categoryReader.GetAll();
            if (!categories.Any()) return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            var id = Guid.NewGuid();

            try
            {
                await mediator.Send(new CreateCategoryCommand(id, request.ParentCategoryID, request.Title));
            }
            catch (Exception ex) when (ex is ValidationException | ex is CategoryNotFoundException)
            {
                return BadRequest(ex.Message);
            }

            var category = categoryReader.GetByIDs(new[] {id}).Single();

            return Created($"/category/id/{id}", category);
        }
    }
}