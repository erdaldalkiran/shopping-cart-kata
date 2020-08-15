using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CampaignTests.When_checking_campaign_applicability_to_line_item
{
    internal class And_campaign_category_is_not_matching
    {
        [Test]
        public void campaign_should_not_be_applicable()
        {
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, Guid.NewGuid()), 3);
            var lineItem = cart.GetLineItems().First();

            var campaign = new Campaign(Guid.NewGuid(), Guid.NewGuid(), 1, DiscountType.Amount, 5m);

            var isApplicable = campaign.IsApplicable(lineItem);
            isApplicable.Should().BeFalse();
        }
    }
}