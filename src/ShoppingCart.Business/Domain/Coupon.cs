using System;

namespace ShoppingCart.Business.Domain
{
    public class Coupon
    {
        public Guid ID { get; }

        public decimal MinimumCartAmount { get; }

        public Coupon(Guid id, Guid categoryId, decimal minimumCartAmount)
        {
            ID = id;
            MinimumCartAmount = Math.Round(minimumCartAmount, 2);
        }
    }
}