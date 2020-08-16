using System;

namespace ShoppingCart.Business.Product
{
    public class Product
    {
        public Guid ID { get; }

        public string Title { get; }

        public decimal Price { get; }

        public Guid CategoryID { get; }

        public Product(Guid id, string title, decimal price, Guid categoryId)
        {
            ID = id;
            Title = title;
            Price = Math.Round(price, 2);
            CategoryID = categoryId;
        }
    }
}