using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Campaign
{
    public interface ICampaignReader
    {
        ICollection<Campaign> GetByCategories(ICollection<Guid> categoryIDs);
    }
}