using System;
using MediatR;

namespace ShoppingCart.Business.Cart
{
    public class ApplyCouponCommand : IRequest
    {
        public Guid ID { get; }
        public Guid CouponID { get; }

        public ApplyCouponCommand(Guid id, Guid couponID)
        {
            ID = id;
            CouponID = couponID;
        }
    }
}