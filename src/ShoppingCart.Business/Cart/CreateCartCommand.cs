using System;
using MediatR;

namespace ShoppingCart.Business.Cart
{
    public class CreateCartCommand : IRequest
    {
        public Guid ID { get; }

        public CreateCartCommand(Guid id)
        {
            ID = id;
        }
    }
}