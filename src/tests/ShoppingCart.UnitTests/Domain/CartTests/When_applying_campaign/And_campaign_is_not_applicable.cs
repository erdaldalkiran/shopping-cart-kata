using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_applying_campaign
{
    internal class And_campaign_is_not_applicable
    {
        private Cart cart;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupData();
        }

        [Test]
        public void line_items_campaign_discount_should_be_0()
        {
            cart.LineItems.ToList()
                .ForEach(l => l.CampaignDiscount.Should().Be(0m));
        }

        private void SetupData()
        {
            cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, Guid.NewGuid()), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 40m, Guid.NewGuid()), 1);

            var campaign = new Campaign(Guid.NewGuid(), Guid.NewGuid(), 1, DiscountType.Rate, 0.10m);

            cart.ApplyCampaign(campaign);
        }
    }
}