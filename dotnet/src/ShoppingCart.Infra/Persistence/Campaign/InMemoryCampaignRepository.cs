using System.Collections.Generic;
using ShoppingCart.Business.Campaign;

namespace ShoppingCart.Infra.Persistence.Campaign
{
    public class InMemoryCampaignRepository : ICampaignRepository
    {
        private readonly ICollection<Business.Campaign.Campaign> campaigns;

        public InMemoryCampaignRepository(ICollection<Business.Campaign.Campaign> campaigns)
        {
            this.campaigns = campaigns;
        }

        public void Add(Business.Campaign.Campaign campaign)
        {
            campaigns.Add(campaign);
        }
    }
}