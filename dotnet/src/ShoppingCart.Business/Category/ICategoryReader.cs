using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Category
{
    public interface ICategoryReader
    {
        IReadOnlyCollection<Category> GetByIDs(ICollection<Guid> ids);
        IReadOnlyCollection<Category> GetAll();
    }
}