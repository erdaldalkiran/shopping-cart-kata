using System;

namespace ShoppingCart.Business.Domain
{
    public class Product
    {
        public Guid Id { get; }

        public string Title { get; }

        public Price Price { get; }

        public Guid CategoryID { get; }

        public Product(Guid id, string title, Price price, Guid categoryId)
        {
            Id = id;
            Title = title;
            Price = price;
            CategoryID = categoryId;
        }
    }
}