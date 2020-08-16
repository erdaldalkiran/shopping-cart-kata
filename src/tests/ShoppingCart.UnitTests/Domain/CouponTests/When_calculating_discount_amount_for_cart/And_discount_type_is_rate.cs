using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CouponTests.When_calculating_discount_amount_for_cart
{
    internal class And_discount_type_is_rate
    {
        [Test]
        public void discount_amount_should_be_calculated_correctly()
        {
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, Guid.NewGuid()), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 40m, Guid.NewGuid()), 2);

            var coupon = new Coupon(Guid.NewGuid(), 20m, DiscountType.Rate, 0.10m);

            var discountAmount = coupon.CalculateDiscountAmount(cart);

            discountAmount.Value.Should().Be(cart.TotalAmount * coupon.Rate);
        }
    }
}