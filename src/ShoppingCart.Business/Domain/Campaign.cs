using System;

namespace ShoppingCart.Business.Domain
{
    public class Campaign
    {
        public Guid Id { get; }

        public Guid CategoryID { get; }

        public int MinimumProductCount { get; }

        public DiscountType DiscountType { get; }

        public Campaign(Guid id, Guid categoryId, int minimumProductCount, DiscountType discountType)
        {
            Id = id;
            CategoryID = categoryId;
            MinimumProductCount = minimumProductCount;
            DiscountType = discountType;
        }
    }

    public enum DiscountType
    {
        Unknown = 0,
        Rate,
        Amount
    }
}