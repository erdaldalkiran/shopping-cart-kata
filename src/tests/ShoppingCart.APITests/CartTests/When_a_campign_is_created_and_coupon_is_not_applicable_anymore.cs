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
    internal class When_a_campign_is_created_and_coupon_is_not_applicable_anymore
    {
        private readonly ApiTestHelper apiHelper = new ApiTestHelper();
        private Guid campaignCategoryID;

        private Cart beforeCampaignCart;
        private Cart afterCampaignCart;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var cartID = await SetupData();

            beforeCampaignCart = await apiHelper.GetCartByID(cartID);

            await apiHelper.CreateACampaign(new CreateCampaignRequest
            {
                CategoryID = campaignCategoryID,
                MinimumItemCount = 1,
                Rate = 9m,
                Type = DiscountType.Amount
            });

            afterCampaignCart = await apiHelper.GetCartByID(cartID);
        }

        [Test]
        public void cart_should_discard_the_coupon()
        {
            beforeCampaignCart.CouponDiscount.Should().Be(10m);
            beforeCampaignCart.AppliedCoupon.Should().NotBeNull();

            afterCampaignCart.CouponDiscount.Should().Be(0m);
            afterCampaignCart.AppliedCoupon.Should().BeNull();
        }

        [Test]
        public void line_items_should_not_have_coupon()
        {
            foreach (var lineItem in afterCampaignCart.LineItems) lineItem.CouponDiscount.Should().Be(0m);
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