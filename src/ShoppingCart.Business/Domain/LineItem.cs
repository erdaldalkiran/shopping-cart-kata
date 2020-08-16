using System;

namespace ShoppingCart.Business.Domain
{
    public class LineItem
    {
        public Product Product { get; }
        public int Quantity { get; }
        public Campaign AppliedCampaign { get; private set; }
        public decimal CampaignDiscount => AppliedCampaign?.CalculateDiscountAmount(this) ?? 0m;
        public decimal CouponDiscount { get; private set; }
        public decimal TotalDiscount => Math.Round(CampaignDiscount + CouponDiscount, 2);
        public decimal TotalAmount => Math.Round(Product.Price * Quantity, 2);
        public decimal TotalAmountAfterCampaignDiscount => TotalAmount - CampaignDiscount;

        public LineItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public void ClearCampaign()
        {
            AppliedCampaign = null;
        }

        public void ApplyCampaign(Campaign campaign)
        {
            var isApplicable = campaign.IsApplicable(this);
            if (!isApplicable) return;
            AppliedCampaign = campaign;
        }

        public void ClearCouponDiscount()
        {
            CouponDiscount = 0m;
        }

        public void SetCouponDiscount(decimal amount)
        {
            CouponDiscount = Math.Round(amount, 2);
        }
    }
}