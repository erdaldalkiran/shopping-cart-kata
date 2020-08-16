using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.Infra.Persistence.Cart
{
    public class InMemoryCartReader : ICartReader
    {
        private readonly ICollection<Business.Cart.Cart> carts;

        public InMemoryCartReader(ICollection<Business.Cart.Cart> carts)
        {
            this.carts = carts;
        }

        public IReadOnlyCollection<Business.Cart.Cart> GetByIDs(ICollection<Guid> ids)
        {
            return carts.Where(c => ids.Contains(c.ID)).ToList();
        }

        public IReadOnlyCollection<Business.Cart.Cart> GetAll()
        {
            return carts.ToList();
        }
    }
}