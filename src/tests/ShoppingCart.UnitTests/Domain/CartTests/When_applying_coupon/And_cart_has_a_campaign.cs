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
    [Description("applying %10 discount all line items in the cart. than applying %20 discount on cart.")]
    internal class And_cart_has_a_campaign
    {
        private Cart cart;
        private Coupon coupon;
        private readonly decimal expectedCartTotal = 380m;
        private readonly decimal expectedCartCampaignDiscount = 38m;
        private readonly decimal expectedCartCouponDiscount = 68.4m;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupData();
            cart.ApplyCoupon(coupon);
        }

        [Test]
        public void line_items_coupon_discount_should_be_correct()
        {
            var items = cart
                .LineItems
                .ToList();

            items.Where(i => i.CouponDiscount == expectedCartCouponDiscount).Should().HaveCount(1);
            items.Where(i => i.CouponDiscount == 0m).Should().HaveCount(1);
        }

        [Test]
        public void cart_total_amount_after_discounts_should_be_correct()
        {
            cart.TotalAmountAfterDiscounts.Should()
                .Be(Math.Round(expectedCartTotal - expectedCartCampaignDiscount - expectedCartCouponDiscount, 2));
        }

        [Test]
        public void cart_coupon_discounts_should_be_correct()
        {
            cart.CouponDiscount.Should().Be(expectedCartCouponDiscount);
        }

        private void SetupData()
        {
            var categoryID = Guid.NewGuid();

            cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, categoryID), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 40m, categoryID), 2);

            var campaign = new Campaign(Guid.NewGuid(), categoryID, 1, DiscountType.Rate, 0.10m);
            cart.ApplyCampaign(campaign);

            coupon = new Coupon(Guid.NewGuid(), 20m, DiscountType.Rate, 0.20m);
        }
    }
}