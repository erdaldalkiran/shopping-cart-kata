using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_applying_campaign.And_cart_has_a_coupon
{
    [Description("cart already has a coupon with discount amount 10 TL. " +
                 "after applying campaign we expect both campaign and coupon are applied to the cart.")]
    internal class And_coupon_is_applicable_after_campaign
    {
        private Cart cart;
        private Campaign campaign;
        private readonly decimal expectedCartTotal = 240m;
        private readonly decimal expectedCartCampaignDiscount = 20m;
        private readonly decimal expectedCartCouponDiscount = 10m;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupData();
            cart.ApplyCampaign(campaign);
        }

        [Test]
        public void line_items_coupon_discount_should_be_correct()
        {
            var items = cart
                .GetLineItems()
                .ToList();

            items.Where(i => i.CouponDiscount == expectedCartCouponDiscount).Should().HaveCount(1);
            items.Where(i => i.CouponDiscount == 0m).Should().HaveCount(1);
        }

        [Test]
        public void line_items_campaign_discount_should_be_correct()
        {
            var items = cart
                .GetLineItems()
                .ToList();

            items.All(i => i.CampaignDiscount == i.Quantity * campaign.Rate).Should().BeTrue();
        }

        [Test]
        public void cart_total_amount_after_discounts_should_be_correct()
        {
            cart.TotalAmountAfterDiscounts.Should()
                .Be(Math.Round(expectedCartTotal - expectedCartCampaignDiscount - expectedCartCouponDiscount, 2));
        }

        [Test]
        public void cart_coupon_discount_should_be_correct()
        {
            cart.CouponDiscount.Should().Be(expectedCartCouponDiscount);
        }

        [Test]
        public void cart_campaign_discount_should_be_correct()
        {
            cart.CampaignDiscount.Should().Be(expectedCartCampaignDiscount);
        }

        private void SetupData()
        {
            var categoryID = Guid.NewGuid();

            cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 100m, categoryID), 2);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 20m, categoryID), 2);

            var coupon = new Coupon(Guid.NewGuid(), 20m, DiscountType.Amount, 10m);
            cart.ApplyCoupon(coupon);

            campaign = new Campaign(Guid.NewGuid(), categoryID, 1, DiscountType.Amount, 5m);
        }
    }
}