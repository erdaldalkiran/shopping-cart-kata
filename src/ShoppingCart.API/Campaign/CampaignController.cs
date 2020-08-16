using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Category;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.API.Campaign
{
    [ApiController]
    [Route("campaign")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignReader campaignReader;
        private readonly IMediator mediator;

        public CampaignController(ICampaignReader campaignReader, IMediator mediator)
        {
            this.campaignReader = campaignReader;
            this.mediator = mediator;
        }

        [HttpGet]
        [Route("id/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get(Guid id)
        {
            var category = campaignReader.GetByIDs(new[] {id}).SingleOrDefault();
            if (category == null) return NotFound();

            return Ok(category);
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAll()
        {
            var categories = campaignReader.GetAll();
            if (!categories.Any()) return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateCampaignRequest request)
        {
            var id = Guid.NewGuid();

            //TODO: add error handling middleware
            try
            {
                await mediator.Send(new CreateCampaignCommand(id, request.CategoryID, request.MinimumItemCount,
                    request.Type, request.Rate));
            }
            catch (Exception ex) when (ex is ValidationException | ex is CategoryNotFoundException)
            {
                return BadRequest(ex.Message);
            }

            var campaign = campaignReader.GetByIDs(new[] {id}).Single();

            return Created($"/campaign/id/{id}", campaign);
        }
    }
}