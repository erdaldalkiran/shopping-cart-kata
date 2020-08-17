using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Business.Cart
{
    public class Cart
    {
        private readonly List<LineItem> lineItems;

        public Guid ID { get; }
        public Coupon.Coupon AppliedCoupon { get; private set; }
        public decimal CouponDiscount { get; private set; }
        public decimal DeliveryCost { get; private set; }

        public Cart(Guid id)
        {
            ID = id;
            lineItems = new List<LineItem>();
        }

        public IReadOnlyCollection<LineItem> LineItems => lineItems;

        public decimal TotalAmount
        {
            get
            {
                var amount = lineItems
                    .Select(l => l.TotalAmount)
                    .Aggregate(0m, (acc, p) => acc + p);

                return Math.Round(amount, 2);
            }
        }

        public decimal CampaignDiscount
        {
            get
            {
                var totalDiscount = lineItems
                    .Select(l => l.CampaignDiscount)
                    .Aggregate(0m, (acc, p) => acc + p);

                return Math.Round(totalDiscount, 2);
            }
        }


        public decimal TotalAmountAfterDiscounts => Math.Round(TotalAmount - CampaignDiscount - CouponDiscount, 2);

        public decimal TotalAmountAfterCampaign => Math.Round(TotalAmount - CampaignDiscount, 2);

        public bool IsEmpty => !LineItems.Any();

        public int GetLineItemsCount()
        {
            return lineItems.Count;
        }

        public int GetDistinctCategoriesCount()
        {
            return lineItems.Select(l => l.Product.CategoryID).Distinct().Count();
        }

        public void AddItem(Product.Product product, int quantity)
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

            ClearCampaign();

            var isApplicable = campaign.IsApplicable(this);
            if (!isApplicable) return;

            lineItems.ForEach(l => l.ApplyCampaign(campaign));
        }

        public void ClearCampaign()
        {
            lineItems.ForEach(l => l.ClearCampaign());
        }

        public void ApplyCoupon(Coupon.Coupon coupon)
        {
            if (coupon == null) throw new ArgumentNullException($"{nameof(coupon)} cannot be null.");

            ClearCoupon();
            var isApplicable = coupon.IsApplicable(this);
            if (!isApplicable) return;
            AppliedCoupon = coupon;
            CouponDiscount = coupon.CalculateDiscountAmount(this).Value;
            DistributeCouponDiscountToLineItems();
        }

        public void SetDeliveryCost(decimal cost)
        {
            if (cost <= 0m) throw new ArgumentNullException($"{nameof(cost)} must be greater than 0.");

            DeliveryCost = Math.Round(cost, 2);
        }

        private void ClearCoupon()
        {
            AppliedCoupon = null;
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