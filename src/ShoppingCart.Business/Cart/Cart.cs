using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Catalog;

namespace ShoppingCart.Business.Cart
{
    public class Cart
    {
        private Guid ID { get; }
        private readonly List<LineItem> lineItems;
        private Coupon.Coupon appliedCoupon;
        private decimal deliveryCost;

        public Cart(Guid id)
        {
            ID = id;
            lineItems = new List<LineItem>();
        }

        public IReadOnlyCollection<LineItem> GetLineItems()
        {
            return lineItems;
        }

        public int GetLineItemsCount()
        {
            return lineItems.Count;
        }

        public int GetDistinctCategoriesCount()
        {
            return lineItems.Select(l => l.Product.CategoryID).Distinct().Count();
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException($"{nameof(product)} cannot be null.");

            if (quantity < 1) throw new ArgumentException($"{nameof(quantity)} must be greater than 0.");

            var inCartProductMaybe = lineItems.FirstOrDefault(l => l.Product.ID == product.ID);
            if (inCartProductMaybe == null)
            {
                lineItems.Add(new LineItem(product, quantity));
                return;
            }

            //ASSUMPTION: last added product has the most current information
            lineItems.Remove(inCartProductMaybe);

            var inCartProductQuantity = inCartProductMaybe.Quantity;
            lineItems.Add(new LineItem(product, quantity + inCartProductQuantity));
        }

        public void ApplyCampaign(Campaign.Campaign campaign)
        {
            if (campaign == null) throw new ArgumentNullException($"{nameof(campaign)} cannot be null.");

            lineItems.ForEach(l => l.ClearCampaign());

            var isApplicable = campaign.IsApplicable(this);
            if (!isApplicable) return;

            lineItems.ForEach(l => l.ApplyCampaign(campaign));

            if (appliedCoupon != null) ApplyCoupon(appliedCoupon);
        }

        public void ApplyCoupon(Coupon.Coupon coupon)
        {
            if (coupon == null) throw new ArgumentNullException($"{nameof(coupon)} cannot be null.");

            ClearCoupon();
            var isApplicable = coupon.IsApplicable(this);
            if (!isApplicable) return;
            appliedCoupon = coupon;
            CouponDiscount = coupon.CalculateDiscountAmount(this).Value;
            DistributeCouponDiscountToLineItems();
        }

        public void SetDeliveryCost(decimal cost)
        {
            if (cost <= 0m) throw new ArgumentNullException($"{nameof(cost)} must be greater than 0.");

            deliveryCost = Math.Round(cost, 2);
        }

        public decimal GetDeliveryCost()
        {
            return deliveryCost;
        }

        public decimal TotalAmount
        {
            get
            {
                var amount = lineItems
                    .Select(l => l.TotalAmount)
                    .Aggregate((acc, p) => acc + p);

                return Math.Round(amount, 2);
            }
        }

        public decimal CampaignDiscount
        {
            get
            {
                var totalDiscount = lineItems
                    .Select(l => l.CampaignDiscount)
                    .Aggregate((acc, p) => acc + p);

                return Math.Round(totalDiscount, 2);
            }
        }

        public decimal CouponDiscount { get; private set; }

        public decimal TotalAmountAfterDiscounts => Math.Round(TotalAmount - CampaignDiscount - CouponDiscount, 2);

        public decimal TotalAmountAfterCampaign => Math.Round(TotalAmount - CampaignDiscount, 2);

        private void ClearCoupon()
        {
            appliedCoupon = null;
            CouponDiscount = 0m;
            lineItems.ForEach(l => l.ClearCouponDiscount());
        }

        private void DistributeCouponDiscountToLineItems()
        {
            var remainingAmount = CouponDiscount;
            foreach (var lineItem in lineItems)
            {
                if (lineItem.TotalAmountAfterCampaignDiscount >= remainingAmount)
                {
                    lineItem.SetCouponDiscount(remainingAmount);
                    remainingAmount = 0;
                    break;
                }

                remainingAmount -= lineItem.TotalAmountAfterCampaignDiscount;
                lineItem.SetCouponDiscount(lineItem.TotalAmountAfterCampaignDiscount);
            }
        }
    }
}