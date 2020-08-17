using System;

namespace ShoppingCart.API.Cart
{
    public class AddItemRequest
    {
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
}