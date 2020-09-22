using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Cart
{
    public interface ICartRepository
    {
        void Add(Cart cart);
        Cart GetByID(Guid id);
        ICollection<Cart> GetAll();
    }
}