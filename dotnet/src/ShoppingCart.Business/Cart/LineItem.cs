﻿using System;

namespace ShoppingCart.Business.Cart
{
    public class LineItem
    {
        public Product.Product Product { get; }
        public int Quantity { get; }
        public Campaign.Campaign AppliedCampaign { get; private set; }
        public decimal CampaignDiscount => AppliedCampaign?.CalculateDiscountAmountFor(this) ?? 0m;
        public decimal CouponDiscount { get; private set; }
        public decimal TotalDiscount => Math.Round(CampaignDiscount + CouponDiscount, 2);
        public decimal TotalAmount => Math.Round(Product.Price * Quantity, 2);
        public decimal TotalAmountAfterCampaignDiscount => Math.Round(TotalAmount - CampaignDiscount, 2);
        public decimal TotalAmountAfterDiscounts => Math.Round(TotalAmount - TotalDiscount, 2);

        public LineItem(Product.Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public void ClearCampaign()
        {
            AppliedCampaign = null;
        }

        public void ApplyCampaign(Campaign.Campaign campaign)
        {
            var isApplicable = campaign.IsApplicableTo(this);
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