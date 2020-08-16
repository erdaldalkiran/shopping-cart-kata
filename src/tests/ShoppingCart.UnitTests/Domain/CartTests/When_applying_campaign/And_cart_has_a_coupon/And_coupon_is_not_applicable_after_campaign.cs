using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Catalog;
using ShoppingCart.Business.Coupon;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_applying_campaign.And_cart_has_a_coupon
{
    [Description("cart already has a coupon with discount amount 10 TL. " +
                 "after applying campaign we expect the campaign is applied but not coupon to the cart.")]
    internal class And_coupon_is_not_applicable_after_campaign
    {
        private Cart cart;
        private Campaign campaign;
        private readonly decimal expectedCartTotal = 280m;
        private readonly decimal expectedCartCampaignDiscount = 80m;

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

            items.All(i => i.CouponDiscount == 0m).Should().BeTrue();
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
                .Be(Math.Round(expectedCartTotal - expectedCartCampaignDiscount, 2));
        }

        [Test]
        public void cart_coupon_discount_should_be_0()
        {
            cart.CouponDiscount.Should().Be(0m);
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
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 40m, categoryID), 2);

            var coupon = new Coupon(Guid.NewGuid(), 200m, DiscountType.Amount, 20m);
            cart.ApplyCoupon(coupon);

            campaign = new Campaign(Guid.NewGuid(), categoryID, 1, DiscountType.Amount, 20m);
        }
    }
}