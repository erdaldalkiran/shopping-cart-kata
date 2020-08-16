using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CouponTests.When_calculating_discount_amount_for_cart
{
    internal class And_discount_type_is_amount_and_cart_total_is_less_than_discount_rate
    {
        [Test]
        public void discount_amount_should_be_cart_total_after_campaign()
        {
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 10m, Guid.NewGuid()), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 20m, Guid.NewGuid()), 2);

            var coupon = new Coupon(Guid.NewGuid(), 20m, DiscountType.Amount, 100m);

            var discountAmount = coupon.CalculateDiscountAmount(cart);

            discountAmount.Value.Should().Be(cart.TotalAmountAfterCampaign);
        }
    }
}