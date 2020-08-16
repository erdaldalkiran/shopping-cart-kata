using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Domain
{
    public class Coupon
    {
        public Guid ID { get; }

        public decimal MinimumCartAmount { get; }

        public DiscountType Type { get; }

        public decimal Rate { get; }

        public Coupon(Guid id, decimal minimumCartAmount, DiscountType type, decimal rate)
        {
            ID = id;
            MinimumCartAmount = Math.Round(minimumCartAmount, 2);
            Type = type;
            Rate = rate;
        }

        public bool IsApplicable(Cart cart)
        {
            var cartAmount = cart.TotalAmountAfterCampaign;

            return cartAmount > MinimumCartAmount;
        }

        public decimal? CalculateDiscountAmount(Cart cart)
        {
            var isApplicable = IsApplicable(cart);
            if (!isApplicable) return null;

            var cartAmount = cart.TotalAmountAfterCampaign;

            return Math.Min(cart.TotalAmountAfterCampaign,
                CouponDiscountAmountCalculator.Strategies[Type](cartAmount, Rate));
        }
    }

    public static class CouponDiscountAmountCalculator
    {
        public static Dictionary<DiscountType, Func<decimal, decimal, decimal>> Strategies =
            new Dictionary<DiscountType, Func<decimal, decimal, decimal>>
            {
                {
                    DiscountType.Amount, (amount, rate) =>
                    {
                        var discountAmount = rate;
                        return Math.Round(discountAmount, 2);
                    }
                },
                {
                    DiscountType.Rate, (amount, rate) =>
                    {
                        var discountAmount = amount * rate;
                        return Math.Round(discountAmount, 2);
                    }
                }
            };
    }
}