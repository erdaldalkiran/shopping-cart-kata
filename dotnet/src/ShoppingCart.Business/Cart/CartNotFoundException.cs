using System;

namespace ShoppingCart.Business.Cart
{
    public class CartNotFoundException : Exception
    {
        public CartNotFoundException(Guid id)
            : base($"cart {id} not found")
        {
        }
    }
}