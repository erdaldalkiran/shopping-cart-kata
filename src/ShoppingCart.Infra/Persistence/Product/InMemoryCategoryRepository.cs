using System.Collections.Generic;
using ShoppingCart.Business.Product;

namespace ShoppingCart.Infra.Persistence.Product
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ICollection<Business.Product.Product> products;

        public InMemoryProductRepository(ICollection<Business.Product.Product> products)
        {
            this.products = products;
        }

        public void Add(Business.Product.Product Product)
        {
            products.Add(Product);
        }
    }
}