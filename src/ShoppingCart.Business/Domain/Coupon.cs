using System;

namespace ShoppingCart.Business.Domain
{
    public class Coupon
    {
        public Guid Id { get; }

        public Price MinimumCartAmount { get; }

        public Coupon(Guid id, Guid categoryId, Price minimumCartAmount)
        {
            Id = id;
            MinimumCartAmount = minimumCartAmount;
        }
    }
}