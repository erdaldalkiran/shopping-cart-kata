using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_applying_coupon
{
    internal class And_cart_has_no_campaign
    {
        private Cart cart;
        private Coupon coupon;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupData();
        }

        [Test]
        public void line_items_coupon_discount_should_be_correct()
        {
            var items = cart
                .LineItems
                .ToList();

            items.Where(i => i.CouponDiscount == coupon.Rate).Should().HaveCount(1);
            items.Where(i => i.CouponDiscount == 0m).Should().HaveCount(1);
        }

        [Test]
        public void cart_total_amount_after_discounts_should_be_correct()
        {
            var expectedTotalDiscountAmount = coupon.Rate;
            var expectedTotalAmount = 340m;

            cart.TotalAmountAfterDiscounts.Should()
                .Be(Math.Round(expectedTotalAmount - expectedTotalDiscountAmount, 2));
        }

        [Test]
        public void cart_coupon_discounts_should_be_correct()
        {
            cart.CouponDiscount.Should().Be(coupon.Rate);
        }

        private void SetupData()
        {
            cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, Guid.NewGuid()), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 40m, Guid.NewGuid()), 1);

            coupon = new Coupon(Guid.NewGuid(), 20m, DiscountType.Amount, 20m);

            cart.ApplyCoupon(coupon);
        }
    }
}