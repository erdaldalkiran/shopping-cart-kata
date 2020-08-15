using System;

namespace ShoppingCart.Business.Domain
{
    public class LineItem
    {
        public Product Product { get; }
        public int Quantity { get; }
        public Campaign AppliedCampaign { get; private set; }
        public decimal TotalDiscount => AppliedCampaign?.CalculateDiscountAmount(this) ?? 0m;
        public decimal TotalAmount => Math.Round(Product.Price * Quantity, 2);

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
    }
}