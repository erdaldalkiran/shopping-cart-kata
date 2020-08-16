using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Campaign
{
    public interface ICampaignReader
    {
        ICollection<Campaign> GetByCategories(ICollection<Guid> categoryIDs);
        IReadOnlyCollection<Campaign> GetByIDs(ICollection<Guid> ids);
        IReadOnlyCollection<Campaign> GetAll();
    }
}