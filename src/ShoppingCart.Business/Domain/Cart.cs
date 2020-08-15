using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Business.Domain
{
    public class Cart
    {
        private Guid ID { get; }
        private readonly List<LineItem> lineItems;

        public Cart(Guid id)
        {
            ID = id;
            lineItems = new List<LineItem>();
        }

        public IReadOnlyCollection<LineItem> GetLineItems()
        {
            return lineItems;
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

        public void ApplyCampaign(Campaign campaign)
        {
            var isApplicable = campaign.IsApplicable(this);
            if (!isApplicable) return;
            ReplaceCampaign(campaign);
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

        public decimal CampaignDiscounts
        {
            get
            {
                var totalDiscount = lineItems
                    .Select(l => l.TotalDiscount)
                    .Aggregate((acc, p) => acc + p);

                return Math.Round(totalDiscount, 2);
            }
        }

        public decimal TotalAmountAfterDiscounts => Math.Round(TotalAmount - CampaignDiscounts, 2);

        private void ReplaceCampaign(Campaign campaign)
        {
            lineItems.ForEach(l => l.ClearCampaign());
            lineItems.ForEach(l => l.ApplyCampaign(campaign));
        }

    }
}