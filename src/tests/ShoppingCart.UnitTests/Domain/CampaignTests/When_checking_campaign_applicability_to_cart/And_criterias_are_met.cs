using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Catalog;

namespace ShoppingCart.UnitTests.Domain.CampaignTests.When_checking_campaign_applicability_to_cart
{
    internal class And_criterias_are_met
    {
        [Test]
        public void campaign_should_be_applicable()
        {
            var categoryID = Guid.NewGuid();
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, categoryID), 1);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 200m, categoryID), 1);

            var campaign = new Campaign(Guid.NewGuid(), categoryID, 1, DiscountType.Amount, 25m);

            var isApplicable = campaign.IsApplicable(cart);
            isApplicable.Should().BeTrue();
        }
    }
}