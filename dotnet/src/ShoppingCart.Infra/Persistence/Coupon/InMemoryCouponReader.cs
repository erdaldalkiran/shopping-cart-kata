using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Coupon;

namespace ShoppingCart.Infra.Persistence.Coupon
{
    public class InMemoryCouponReader : ICouponReader
    {
        private readonly ICollection<Business.Coupon.Coupon> coupons;

        public InMemoryCouponReader(ICollection<Business.Coupon.Coupon> coupons)
        {
            this.coupons = coupons;
        }

        public IReadOnlyCollection<Business.Coupon.Coupon> GetByIDs(ICollection<Guid> ids)
        {
            return coupons.Where(c => ids.Contains(c.ID)).ToList();
        }

        public IReadOnlyCollection<Business.Coupon.Coupon> GetAll()
        {
            return coupons.ToList();
        }
    }
}