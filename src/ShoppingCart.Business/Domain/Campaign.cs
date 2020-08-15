using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Business.Domain
{
    public class Campaign
    {
        public Guid ID { get; }

        public Guid CategoryID { get; }

        public int MinimumProductCount { get; }

        public DiscountType Type { get; }

        public decimal Rate { get; }

        public Campaign(Guid id, Guid categoryId, int minimumProductCount, DiscountType type, decimal rate)
        {
            ID = id;
            CategoryID = categoryId;
            MinimumProductCount = minimumProductCount;
            Type = type;
            Rate = Math.Round(rate, 2);
        }

        public decimal? CalculateDiscountAmount(Cart cart)
        {
            var isApplicable = IsApplicable(cart);
            if (!isApplicable)
            {
                return null;
            }

            var items = GetCampaignApplicableLineItems(cart);

            var sum = items.Sum(i => DiscountAmountCalculator.Strategies[Type](i, Rate));
            return Math.Round(sum, 2);
        }

        public decimal? CalculateDiscountAmount(LineItem lineItem)
        {
            var isApplicable = IsApplicable(lineItem);
            if (!isApplicable)
            {
                return null;
            }

            return DiscountAmountCalculator.Strategies[Type](lineItem, Rate);
        }

        public bool IsApplicable(Cart cart)
        {
            var items = GetCampaignApplicableLineItems(cart);
            var minimumCountRequirement = DoesCartContainMinimumProductCount(items);


            return minimumCountRequirement;
        }

        public bool IsApplicable(LineItem lineItem)
        {
            var categoryRequirement = lineItem.Product.CategoryID == CategoryID;
            var priceRequirement = true;
            if (Type == DiscountType.Amount)
            {
                priceRequirement = lineItem.Product.Price > Rate;
            }

            return categoryRequirement & priceRequirement;
        }

        private List<LineItem> GetCampaignApplicableLineItems(Cart cart)
        {
            return cart.GetLineItems().Where(IsApplicable).ToList();
        }

        private bool DoesCartContainMinimumProductCount(List<LineItem> items)
        {
            var itemCount = items.Count;
            if (itemCount >= MinimumProductCount) return true;

            return false;
        }

    }

    public enum DiscountType
    {
        Unknown = 0,
        Rate,
        Amount
    }

    public static class DiscountAmountCalculator
    {
        public static Dictionary<DiscountType, Func<LineItem, decimal, decimal>> Strategies =
            new Dictionary<DiscountType, Func<LineItem, decimal, decimal>>
            {
                {
                    DiscountType.Amount, (lineItem, rate) =>
                    {
                        var discountAmount = lineItem.Quantity * rate;
                        return Math.Round(discountAmount, 2);
                    }
                },
                {
                    DiscountType.Rate, (lineItem, rate) =>
                    {
                        var discountAmount = lineItem.Product.Price * lineItem.Quantity * rate;
                        return Math.Round(discountAmount, 2);
                    }
                }
            };
    }
}