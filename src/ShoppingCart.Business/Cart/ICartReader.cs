using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Cart
{
    public interface ICartReader
    {
        IReadOnlyCollection<Cart> GetByIDs(ICollection<Guid> ids);
        IReadOnlyCollection<Cart> GetAll();
    }
}