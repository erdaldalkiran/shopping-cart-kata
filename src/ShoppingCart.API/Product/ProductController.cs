using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Category;
using ShoppingCart.Business.Product;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.API.Product
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductReader productReader;
        private readonly IMediator mediator;

        public ProductController(IProductReader productReader, IMediator mediator)
        {
            this.productReader = productReader;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(Guid id)
        {
            var category = productReader.GetByIDs(new[] {id}).SingleOrDefault();
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var categories = productReader.GetAll();
            if (!categories.Any()) return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var id = Guid.NewGuid();

            //TODO: add error handling middleware
            try
            {
                await mediator.Send(new CreateProductCommand(id, request.Title, request.Price, request.CategoryID));
            }
            catch (Exception ex) when (ex is ValidationException | ex is CategoryNotFoundException)
            {
                return BadRequest(ex.Message);
            }

            var product = productReader.GetByIDs(new[] {id}).Single();

            return Created($"/product/id/{id}", product);
        }
    }
}