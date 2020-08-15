using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CampaignTests.When_checking_campaign_applicability_to_cart
{
    internal class And_discount_type_is_amount_and_product_price_is_less_than_discount_rate_for_all_line_items
    {
        [Test]
        public void campaign_should_not_be_applicable()
        {
            var categoryID = Guid.NewGuid();
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 10m, categoryID), 1);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 20m, categoryID), 1);

            var campaign = new Campaign(Guid.NewGuid(), categoryID, 1, DiscountType.Amount, 50m);

            var isApplicable = campaign.IsApplicable(cart);
            isApplicable.Should().BeFalse();
        }
    }
}