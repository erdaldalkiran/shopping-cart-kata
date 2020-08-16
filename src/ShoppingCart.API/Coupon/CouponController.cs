using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.API.Coupon
{
    [ApiController]
    [Route("coupon")]
    public class CouponController : ControllerBase
    {
        private readonly ICouponReader couponReader;
        private readonly IMediator mediator;

        public CouponController(ICouponReader couponReader, IMediator mediator)
        {
            this.couponReader = couponReader;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(Guid id)
        {
            var category = couponReader.GetByIDs(new[] {id}).SingleOrDefault();
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var categories = couponReader.GetAll();
            if (!categories.Any()) return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateCouponRequest request)
        {
            var id = Guid.NewGuid();

            //TODO: add error handling middleware
            try
            {
                await mediator.Send(new CreateCouponCommand(id, request.MinimumCartAmount, request.Type, request.Rate));
            }
            catch (Exception ex) when (ex is ValidationException)
            {
                return BadRequest(ex.Message);
            }

            var coupon = couponReader.GetByIDs(new[] {id}).Single();

            return Created($"/coupon/id/{id}", coupon);
        }
    }
}