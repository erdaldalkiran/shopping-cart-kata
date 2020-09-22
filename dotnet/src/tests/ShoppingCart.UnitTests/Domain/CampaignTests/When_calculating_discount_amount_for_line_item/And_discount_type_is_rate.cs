using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CampaignTests.When_calculating_discount_amount_for_line_item
{
    internal class And_discount_type_is_rate
    {
        [Test]
        public void discount_amount_should_be_calculated_correctly()
        {
            var categoryID = Guid.NewGuid();
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 20m, categoryID), 4);
            var lineItem = cart.LineItems.First();

            var campaign = new Campaign(Guid.NewGuid(), categoryID, 1, DiscountType.Rate, 0.10m);

            var discountAmount = campaign.CalculateDiscountAmountFor(lineItem);

            discountAmount.Value.Should().Be(8m);
        }
    }
}