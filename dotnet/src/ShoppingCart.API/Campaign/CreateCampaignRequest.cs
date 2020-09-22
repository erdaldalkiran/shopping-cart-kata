using System;
using ShoppingCart.Business.Campaign;

namespace ShoppingCart.API.Campaign
{
    public class CreateCampaignRequest
    {
        public Guid CategoryID { get; set; }

        public int MinimumItemCount { get; set; }

        public DiscountType Type { get; set; }

        public decimal Rate { get; set; }
    }
}