using System;

namespace ShoppingCart.Business.Product
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid id)
            : base($"product {id} not found")
        {
        }
    }
}