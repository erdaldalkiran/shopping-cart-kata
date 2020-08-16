using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Catalog
{
    public interface ICategoryReader
    {
        ICollection<Category> GetByIDs(ICollection<Guid> ids);
    }
}