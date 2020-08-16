using System;

namespace ShoppingCart.Business.Cart
{
    public interface ICartRepository
    {
        void Add(Cart cart);
        Cart GetByID(Guid id);
    }
}