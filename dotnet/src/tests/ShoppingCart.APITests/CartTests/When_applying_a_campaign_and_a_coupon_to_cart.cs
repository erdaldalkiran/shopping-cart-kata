﻿using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.API.Campaign;
using ShoppingCart.API.Cart;
using ShoppingCart.API.Category;
using ShoppingCart.API.Coupon;
using ShoppingCart.API.Product;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.APITests.CartTests
{
    [Description("there are 2 line items in the cart. campaign is applied to the one of them. then a coupon is applied.")]
    internal class When_applying_a_campaign_and_a_coupon_to_cart
    {
        private readonly ApiTestHelper apiHelper = new ApiTestHelper();
        private Guid campaignCategoryID;

        private Cart cart;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var cartID = await SetupData();
            cart = await apiHelper.GetCartByID(cartID);
        }

        [Test]
        public void cart_should_reflect_correct_values()
        {
            cart.CampaignDiscount.Should().Be(3m);
            cart.CouponDiscount.Should().Be(10m);
            cart.AppliedCoupon.Should().NotBeNull();
            cart.TotalAmount.Should().Be(35m);
            cart.TotalAmountAfterCampaign.Should().Be(32m);
            cart.TotalAmountAfterDiscounts.Should().Be(22m);
        }

        [Test]
        public void campaign_and_coupon_applied_line_item_should_have_correct_values()
        {
            var lineItem =
                cart.LineItems.Single(l => l.Product.CategoryID == campaignCategoryID && l.CouponDiscount != 0m);

            lineItem.AppliedCampaign.Should().NotBeNull();
            lineItem.CampaignDiscount.Should().Be(3m);
            lineItem.CouponDiscount.Should().Be(10m);
            lineItem.TotalAmount.Should().Be(30m);
            lineItem.TotalAmountAfterCampaignDiscount.Should().Be(27m);
            lineItem.TotalAmountAfterDiscounts.Should().Be(17m);
            lineItem.TotalDiscount.Should().Be(13m);
        }

        [Test]
        public void campaign_and_coupon_not_applied_line_item_should_have_correct_values()
        {
            var lineItem = cart.LineItems.Single(l => l.Product.CategoryID != campaignCategoryID && l.CouponDiscount == 0m);

            lineItem.AppliedCampaign.Should().BeNull();
            lineItem.CampaignDiscount.Should().Be(0m);
            lineItem.CouponDiscount.Should().Be(0m);
            lineItem.TotalAmount.Should().Be(5m);
            lineItem.TotalAmountAfterCampaignDiscount.Should().Be(5m);
            lineItem.TotalAmountAfterDiscounts.Should().Be(5m);
            lineItem.TotalDiscount.Should().Be(0m);
        }

        private async Task<Guid> SetupData()
        {
            var cartID = await apiHelper.CreateACart();
            var categoryID1 = await apiHelper.CreateACategory(new CreateCategoryRequest
            {
                Title = "category"
            });
            campaignCategoryID = categoryID1;
            var categoryID2 = await apiHelper.CreateACategory(new CreateCategoryRequest
            {
                Title = "category"
            });

            var campaignID = await apiHelper.CreateACampaign(new CreateCampaignRequest
            {
                CategoryID = campaignCategoryID,
                MinimumItemCount = 1,
                Rate = 0.10m,
                Type = DiscountType.Rate
            });

            var productID1 = await apiHelper.CreateAProduct(new CreateProductRequest
            {
                CategoryID = categoryID1,
                Price = 10m,
                Title = "Product1"
            });
            var productID2 = await apiHelper.CreateAProduct(new CreateProductRequest
            {
                CategoryID = categoryID2,
                Price = 5m,
                Title = "Product2"
            });

            await apiHelper.AddItemToTheCart(cartID, new AddItemRequest
            {
                ProductID = productID1,
                Quantity = 3
            });
            await apiHelper.AddItemToTheCart(cartID, new AddItemRequest
            {
                ProductID = productID2,
                Quantity = 1
            });

            var couponID = await apiHelper.CreateACoupon(new CreateCouponRequest
            {
                MinimumCartAmount = 20m,
                Rate = 10m,
                Type = DiscountType.Amount
            });

            await apiHelper.ApplyCouponTheCart(cartID, new ApplyCouponRequest
            {
                CouponID = couponID
            });

            return cartID;
        }
    }
}