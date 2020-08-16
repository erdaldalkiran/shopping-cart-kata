using System;
using MediatR;

namespace ShoppingCart.Business.Campaign
{
    public class CreateCampaignCommand : IRequest
    {
        public Guid ID { get; }

        public Guid CategoryID { get; }

        public int MinimumItemCount { get; }

        public DiscountType Type { get; }

        public decimal Rate { get; }

        public CreateCampaignCommand(Guid id, Guid categoryId, int minimumItemCount, DiscountType type, decimal rate)
        {
            ID = id;
            CategoryID = categoryId;
            MinimumItemCount = minimumItemCount;
            Type = type;
            Rate = Math.Round(rate, 2);
        }
    }
}