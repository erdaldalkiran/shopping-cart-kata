using System;
using MediatR;

namespace ShoppingCart.Business.Cart
{
    public class AddItemCommand : IRequest
    {
        public Guid ID { get; }
        public Guid ProductID { get; }
        public int Quantity { get; }

        public AddItemCommand(Guid id, Guid productId, int quantity)
        {
            ID = id;
            ProductID = productId;
            Quantity = quantity;
        }
    }
}