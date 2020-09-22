using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Coupon
{
    public interface ICouponReader
    {
        IReadOnlyCollection<Coupon> GetByIDs(ICollection<Guid> ids);
        IReadOnlyCollection<Coupon> GetAll();
    }
}