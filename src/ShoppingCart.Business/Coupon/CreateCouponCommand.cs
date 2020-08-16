using System;
using MediatR;
using ShoppingCart.Business.Campaign;

namespace ShoppingCart.Business.Coupon
{
    public class CreateCouponCommand : IRequest
    {
        public Guid ID { get; }

        public decimal MinimumCartAmount { get; }

        public DiscountType Type { get; }

        public decimal Rate { get; }

        public CreateCouponCommand(Guid id, decimal minimumCartAmount, DiscountType type, decimal rate)
        {
            ID = id;
            MinimumCartAmount = Math.Round(minimumCartAmount, 2);
            Type = type;
            Rate = rate;
        }
    }
}