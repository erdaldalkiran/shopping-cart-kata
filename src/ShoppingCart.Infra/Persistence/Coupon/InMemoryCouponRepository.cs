using System.Collections.Generic;
using ShoppingCart.Business.Coupon;

namespace ShoppingCart.Infra.Persistence.Coupon
{
    public class InMemoryCouponRepository : ICouponRepository
    {
        private readonly ICollection<Business.Coupon.Coupon> coupons;

        public InMemoryCouponRepository(ICollection<Business.Coupon.Coupon> coupons)
        {
            this.coupons = coupons;
        }

        public void Add(Business.Coupon.Coupon coupon)
        {
            coupons.Add(coupon);
        }
    }
}