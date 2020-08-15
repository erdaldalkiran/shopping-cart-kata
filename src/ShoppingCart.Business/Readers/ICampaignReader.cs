using System;
using System.Collections.Generic;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.Business.Readers
{
    public interface ICampaignReader
    {
        ICollection<Campaign> GetByCategories(ICollection<Guid> categoryIDs);
    }
}