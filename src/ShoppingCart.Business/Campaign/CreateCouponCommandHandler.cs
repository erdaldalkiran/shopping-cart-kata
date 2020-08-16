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

        public CreateCampaignCommandHandler(ICampaignRepository repository, ICategoryReader categoryReader)
        {
            this.repository = repository;
            this.categoryReader = categoryReader;
        }

        protected override Task Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var category = categoryReader.GetByIDs(new List<Guid> {request.CategoryID}).FirstOrDefault();
            if (category == null) throw new CategoryNotFoundException(request.CategoryID);

            var campaign = new Campaign(request.ID, request.CategoryID, request.MinimumItemCount, request.Type,
                request.Rate);
            repository.Add(campaign);

            return Task.CompletedTask;
        }
    }
}