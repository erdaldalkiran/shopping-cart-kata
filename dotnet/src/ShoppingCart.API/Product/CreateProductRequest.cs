using System;

namespace ShoppingCart.API.Product
{
    public class CreateProductRequest
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryID { get; set; }
    }
}