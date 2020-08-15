using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CampaignTests.When_calculating_discount_amount_for_cart
{
    [Description("campaign is applied only one line item because other line item has a price less than discount rate.")]
    internal class And_discount_type_is_amount_and_product_price_is_less_than_discount_rate_for_some_line_items
    {
        [Test]
        public void discount_amount_should_be_calculated_correctly()
        {
            var categoryID = Guid.NewGuid();
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 10m, categoryID), 1);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 20m, categoryID), 1);

            var campaign = new Campaign(Guid.NewGuid(), categoryID, 1, DiscountType.Amount, 15m);

            var discountAmount = campaign.CalculateDiscountAmount(cart);

            discountAmount.Value.Should().Be(15m);
        }
    }
}