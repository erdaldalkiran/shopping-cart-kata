using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.Business.Campaign
{
    public class Campaign
    {
        public Guid ID { get; }

        public Guid CategoryID { get; }

        public int MinimumItemCount { get; }

        public DiscountType Type { get; }

        public decimal Rate { get; }

        public Campaign(Guid id, Guid categoryID, int minimumItemCount, DiscountType type, decimal rate)
        {
            ID = id;
            CategoryID = categoryID;
            MinimumItemCount = minimumItemCount;
            Type = type;
            Rate = Math.Round(rate, 2);
        }

        public decimal? CalculateDiscountAmountFor(Cart.Cart cart)
        {
            var isApplicable = IsApplicableTo(cart);
            if (!isApplicable) return null;

            var items = GetCampaignApplicableLineItems(cart);

            var sum = items.Sum(i => CampaignDiscountAmountCalculator.Strategies[Type](i, Rate));
            return Math.Round(sum, 2);
        }

        public decimal? CalculateDiscountAmountFor(LineItem lineItem)
        {
            var isApplicable = IsApplicableTo(lineItem);
            if (!isApplicable) return null;

            return CampaignDiscountAmountCalculator.Strategies[Type](lineItem, Rate);
        }

        public bool IsApplicableTo(Cart.Cart cart)
        {
            var items = GetCampaignApplicableLineItems(cart);
         
            return DoesCartContainMinimumItemCount(items);
        }

        public bool IsApplicableTo(LineItem lineItem)
        {
            var categoryRequirement = lineItem.Product.CategoryID == CategoryID;
            var priceRequirement = true;
            if (Type == DiscountType.Amount) priceRequirement = lineItem.Product.Price > Rate;

            return categoryRequirement && priceRequirement;
        }

        private List<LineItem> GetCampaignApplicableLineItems(Cart.Cart cart)
        {
            return cart.LineItems.Where(IsApplicableTo).ToList();
        }

        private bool DoesCartContainMinimumItemCount(List<LineItem> items)
        {
            var itemCount = items.Sum(i => i.Quantity);
            if (itemCount > MinimumItemCount) return true;

            return false;
        }
    }

    public enum DiscountType
    {
        Unknown = 0,
        Rate,
        Amount
    }

    public static class CampaignDiscountAmountCalculator
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