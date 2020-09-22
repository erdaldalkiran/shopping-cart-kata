using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Campaign
{
    public interface ICampaignReader
    {
        IReadOnlyCollection<Campaign> GetByCategories(ICollection<Guid> categoryIDs);
        IReadOnlyCollection<Campaign> GetByIDs(ICollection<Guid> ids);
        IReadOnlyCollection<Campaign> GetAll();
    }
}