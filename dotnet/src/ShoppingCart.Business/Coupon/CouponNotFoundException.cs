using System;

namespace ShoppingCart.Business.Coupon
{
    public class CouponNotFoundException : Exception
    {
        public CouponNotFoundException(Guid id)
            : base($"coupon {id} not found")
        {
        }
    }
}