using System;
using System.Collections.Generic;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.Business.Readers
{
    public interface ICategoryReader
    {
        ICollection<Category> GetByIDs(ICollection<Guid> ids);
    }
}