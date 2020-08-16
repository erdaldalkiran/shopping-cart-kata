using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Campaign;

namespace ShoppingCart.Infra.Persistence.Campaign
{
    public class InMemoryCampaignReader : ICampaignReader
    {
        private readonly ICollection<Business.Campaign.Campaign> campaigns;

        public InMemoryCampaignReader(ICollection<Business.Campaign.Campaign> campaigns)
        {
            this.campaigns = campaigns;
        }

        public ICollection<Business.Campaign.Campaign> GetByCategories(ICollection<Guid> categoryIDs)
        {
            return campaigns.Where(c => categoryIDs.Contains(c.CategoryID)).ToList();
        }

        public IReadOnlyCollection<Business.Campaign.Campaign> GetByIDs(ICollection<Guid> ids)
        {
            return campaigns.Where(c => ids.Contains(c.ID)).ToList();
        }

        public IReadOnlyCollection<Business.Campaign.Campaign> GetAll()
        {
            return campaigns.ToList();
        }
    }
}