using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Product
{
    public interface IProductReader
    {
        IReadOnlyCollection<Product> GetByIDs(ICollection<Guid> ids);
        IReadOnlyCollection<Product> GetAll();
    }
}