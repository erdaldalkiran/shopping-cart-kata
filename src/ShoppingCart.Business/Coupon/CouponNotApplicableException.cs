using System;

namespace ShoppingCart.Business.Coupon
{
    public class CouponNotApplicableException : Exception
    {
        public CouponNotApplicableException(Guid couponId, Guid cartId)
            : base($"coupon {couponId} is not applicable to the cart {cartId}")
        {
        }
    }
}