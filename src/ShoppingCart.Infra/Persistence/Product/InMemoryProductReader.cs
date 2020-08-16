using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Product;

namespace ShoppingCart.Infra.Persistence.Product
{
    public class InMemoryProductReader : IProductReader
    {
        private readonly ICollection<Business.Product.Product> products;

        public InMemoryProductReader(ICollection<Business.Product.Product> products)
        {
            this.products = products;
        }

        public IReadOnlyCollection<Business.Product.Product> GetByIDs(ICollection<Guid> ids)
        {
            return products.Where(c => ids.Contains(c.ID)).ToList();
        }

        public IReadOnlyCollection<Business.Product.Product> GetAll()
        {
            return products.ToList();
        }
    }
}