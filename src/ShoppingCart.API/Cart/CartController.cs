using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.API.Cart
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartReader cartReader;
        private readonly IMediator mediator;

        public CartController(ICartReader cartReader, IMediator mediator)
        {
            this.cartReader = cartReader;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(Guid id)
        {
            var category = cartReader.GetByIDs(new[] { id }).SingleOrDefault();
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var categories = cartReader.GetAll();
            if (!categories.Any()) return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create()
        {
            var id = Guid.NewGuid();

            await mediator.Send(new CreateCartCommand(id));

            var cart = cartReader.GetByIDs(new[] { id }).Single();

            return Created($"/cart/id/{id}", cart);
        }
    }
}