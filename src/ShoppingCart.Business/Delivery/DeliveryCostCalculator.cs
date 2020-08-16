using System;

namespace ShoppingCart.Business.Delivery
{
    public class DeliveryCostCalculator
    {
        public decimal CostPerDelivery { get; }
        public decimal CostPerProduct { get; }
        public decimal FixedCost => 2.99m;

        public DeliveryCostCalculator(decimal costPerDelivery, decimal costPerProduct)
        {
            CostPerDelivery = costPerDelivery;
            CostPerProduct = costPerProduct;
        }

        public decimal CalculateFor(Cart.Cart cart)
        {
            var distinctProductCount = cart.GetLineItemsCount();
            var distinctCategoryCount = cart.GetDistinctCategoriesCount();

            return Math.Round(
                distinctCategoryCount * CostPerDelivery + distinctProductCount * CostPerProduct + FixedCost, 2);
        }
    }
}