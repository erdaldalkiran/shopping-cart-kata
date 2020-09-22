using System;
using MediatR;

namespace ShoppingCart.Business.Cart
{
    public class AddItemCommand : IRequest
    {
        public Guid ID { get; }
        public Guid ProductID { get; }
        public int Quantity { get; }

        public AddItemCommand(Guid id, Guid productID, int quantity)
        {
            ID = id;
            ProductID = productID;
            Quantity = quantity;
        }
    }
}