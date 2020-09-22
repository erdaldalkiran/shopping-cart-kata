using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.Infra.Persistence.Cart
{
    public class InMemoryCartRepository : ICartRepository
    {
        private readonly ICollection<Business.Cart.Cart> carts;

        public InMemoryCartRepository(ICollection<Business.Cart.Cart> carts)
        {
            this.carts = carts;
        }

        public void Add(Business.Cart.Cart cart)
        {
            carts.Add(cart);
        }

        public Business.Cart.Cart GetByID(Guid id)
        {
            return carts.SingleOrDefault(c => c.ID == id);
        }

        public ICollection<Business.Cart.Cart> GetAll()
        {
            return carts;
        }
    }
}