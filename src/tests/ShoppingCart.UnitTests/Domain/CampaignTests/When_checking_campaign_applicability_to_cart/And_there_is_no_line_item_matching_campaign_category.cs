using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CampaignTests.When_checking_campaign_applicability_to_cart
{
    internal class And_there_is_no_line_item_matching_campaign_category
    {
        [Test]
        public void campaign_should_not_be_applicable()
        {
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, Guid.NewGuid()), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 200m, Guid.NewGuid()), 3);

            var campaign = new Campaign(Guid.NewGuid(), Guid.NewGuid(), 1, DiscountType.Amount, 5m);

            var isApplicable = campaign.IsApplicable(cart);
            isApplicable.Should().BeFalse();
        }
    }
}