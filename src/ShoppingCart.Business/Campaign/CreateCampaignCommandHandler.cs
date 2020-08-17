using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingCart.Business.Category;

namespace ShoppingCart.Business.Campaign
{
    public class CreateCampaignCommandHandler : AsyncRequestHandler<CreateCampaignCommand>
    {
        private readonly ICampaignRepository repository;
        private readonly ICategoryReader categoryReader;
        private readonly IMediator mediator;

        public CreateCampaignCommandHandler(
            ICampaignRepository repository,
            ICategoryReader categoryReader,
            IMediator mediator)
        {
            this.repository = repository;
            this.categoryReader = categoryReader;
            this.mediator = mediator;
        }

        protected override async Task Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var category = categoryReader.GetByIDs(new List<Guid> {request.CategoryID}).FirstOrDefault();
            if (category == null) throw new CategoryNotFoundException(request.CategoryID);

            var campaign = new Campaign(request.ID, request.CategoryID, request.MinimumItemCount, request.Type,
                request.Rate);
            repository.Add(campaign);

            await mediator.Publish(new CampaignCreatedEvent {ID = campaign.ID}, cancellationToken);
        }
    }
}