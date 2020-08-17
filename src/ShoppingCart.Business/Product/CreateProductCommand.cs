using System;
using MediatR;

namespace ShoppingCart.Business.Product
{
    public class CreateProductCommand : IRequest
    {
        public Guid ID { get; }

        public string Title { get; }

        public decimal Price { get; }

        public Guid CategoryID { get; }

        public CreateProductCommand(Guid id, string title, decimal price, Guid categoryID)
        {
            ID = id;
            Title = title;
            Price = Math.Round(price, 2);
            CategoryID = categoryID;
        }
    }
}