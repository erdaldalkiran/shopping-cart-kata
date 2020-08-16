using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_applying_campaign
{
    [Description("campaign is applicable to two line items.")]
    internal class And_campaign_is_applicable
    {
        private Cart cart;
        private readonly Guid categoryID = Guid.NewGuid();
        private Campaign campaign;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupData();
        }

        [Test]
        public void campaign_not_applied_line_items_campaign_discount_should_be_0()
        {
            cart
                .LineItems
                .Where(l => l.Product.CategoryID != categoryID)
                .ToList()
                .ForEach(l => l.CampaignDiscount.Should().Be(0m));
        }

        [Test]
        public void campaign_applied_line_items_campaign_discount_should_be_correct()
        {
            var items = cart
                .LineItems
                .Where(l => l.Product.CategoryID == categoryID)
                .ToList();

            items.ForEach(l =>
            {
                var expectedDiscountAmount = Math.Round(l.Product.Price * l.Quantity * campaign.Rate);
                l.CampaignDiscount.Should().Be(expectedDiscountAmount);
            });
        }

        [Test]
        public void cart_total_amount_after_discounts_should_be_correct()
        {
            var items = cart
                .LineItems
                .ToList();

            var expectedTotalAmount = items.Sum(l =>
            {
                var discountAmount = Math.Round(l.Product.Price * l.Quantity);
                return Math.Round(discountAmount, 2);
            });

            var campaignAppliedItems = items
                .Where(l => l.Product.CategoryID == categoryID)
                .ToList();
            var expectedTotalDiscountAmount = campaignAppliedItems.Sum(l =>
            {
                var discountAmount = Math.Round(l.Product.Price * l.Quantity * campaign.Rate);
                return Math.Round(discountAmount, 2);
            });

            cart.TotalAmountAfterDiscounts.Should()
                .Be(Math.Round(expectedTotalAmount - expectedTotalDiscountAmount, 2));
        }

        [Test]
        public void cart_campaign_discounts_should_be_correct()
        {
            var campaignAppliedItems = cart
                .LineItems
                .Where(l => l.Product.CategoryID == categoryID)
                .ToList();

            var expectedTotalDiscountAmount = campaignAppliedItems.Sum(l =>
            {
                var discountAmount = Math.Round(l.Product.Price * l.Quantity * campaign.Rate);
                return Math.Round(discountAmount, 2);
            });

            cart.CampaignDiscount.Should().Be(expectedTotalDiscountAmount);
        }

        private void SetupData()
        {
            cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, Guid.NewGuid()), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 40m, Guid.NewGuid()), 1);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleC", 20m, categoryID), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleD", 10m, categoryID), 3);

            campaign = new Campaign(Guid.NewGuid(), categoryID, 2, DiscountType.Rate, 0.20m);

            cart.ApplyCampaign(campaign);
        }
    }
}