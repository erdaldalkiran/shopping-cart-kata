using System;
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
    internal class When_a_campaign_is_created
    {
        private readonly ApiTestHelper apiHelper = new ApiTestHelper();
        private Guid campaignCategoryID;
        private Cart cartBeforeCampaign;
        private Cart cartAfterCampaign;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var cartID = await SetupData();
            cartBeforeCampaign = await apiHelper.GetCartByID(cartID);

            await apiHelper.CreateACampaign(new CreateCampaignRequest
            {
                CategoryID = campaignCategoryID,
                MinimumItemCount = 1,
                Rate = 0.10m,
                Type = DiscountType.Rate
            });

            cartAfterCampaign = await apiHelper.GetCartByID(cartID);
        }

        [Test]
        public void campaign_should_applied_to_the_cart()
        {
            cartBeforeCampaign.CampaignDiscount.Should().Be(0m);
            cartAfterCampaign.CampaignDiscount.Should().Be(3m);
        }

        [Test]
        public void applicable_coupon_should_be_kept()
        {
            cartBeforeCampaign.CouponDiscount.Should().Be(10m);
            cartAfterCampaign.CouponDiscount.Should().Be(10m);
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

            var productID1 = await apiHelper.CreateAProduct(new CreateProductRequest
            {
                CategoryID = campaignCategoryID,
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