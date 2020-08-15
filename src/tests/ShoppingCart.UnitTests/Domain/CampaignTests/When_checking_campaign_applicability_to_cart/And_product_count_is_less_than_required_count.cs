using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CampaignTests.When_checking_campaign_applicability_to_cart
{
    internal class And_product_count_is_less_than_required_count
    {
        [Test]
        public void campaign_should_not_be_applicable()
        {
            var categoryID = Guid.NewGuid();
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, categoryID), 1);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 200m, categoryID), 1);

            var campaign = new Campaign(Guid.NewGuid(), categoryID, 3, DiscountType.Rate, 0.15m);

            var isApplicable = campaign.IsApplicable(cart);
            isApplicable.Should().BeFalse();
        }
    }
}