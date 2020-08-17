using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Product;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.API.Cart
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartReader cartReader;
        private readonly CartPrinter cartPrinter;
        private readonly IMediator mediator;

        public CartController(
            ICartReader cartReader,
            CartPrinter cartPrinter,
            IMediator mediator)
        {
            this.cartReader = cartReader;
            this.cartPrinter = cartPrinter;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(Guid id)
        {
            var cart = cartReader.GetByIDs(new[] {id}).SingleOrDefault();
            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var carts = cartReader.GetAll();
            if (!carts.Any()) return NotFound();

            return Ok(carts);
        }

        [HttpGet]
        [Route("id/{id}/print")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Print(Guid id)
        {
            var cart = cartReader.GetByIDs(new[] {id}).SingleOrDefault();
            if (cart == null) return NotFound();

            var result = cartPrinter.Print(cart);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create()
        {
            var id = Guid.NewGuid();

            await mediator.Send(new CreateCartCommand(id));

            var cart = cartReader.GetByIDs(new[] {id}).Single();

            return Created($"/cart/id/{id}", cart);
        }

        [HttpPost]
        [Route("id/{id}/add-item")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] AddItemRequest request)
        {
            try
            {
                await mediator.Send(new AddItemCommand(id, request.ProductID, request.Quantity));
            }
            catch (Exception ex) when (ex is ValidationException || ex is CartNotFoundException ||
                                       ex is ProductNotFoundException)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost]
        [Route("id/{id}/apply-coupon")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ApplyCoupon(Guid id, [FromBody] ApplyCouponRequest request)
        {
            try
            {
                await mediator.Send(new ApplyCouponCommand(id, request.CouponID));
            }
            catch (Exception ex) when (ex is ValidationException || ex is CartNotFoundException ||
                                       ex is CouponNotFoundException
                                       || ex is CouponNotApplicableException)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}