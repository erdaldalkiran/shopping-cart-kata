using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.API.Cart;
using ShoppingCart.API.Category;
using ShoppingCart.API.Coupon;
using ShoppingCart.API.Product;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.APITests.CartTests
{
    internal class When_applying_a_coupon_to_cart
    {
        private readonly ApiTestHelper apiHelper = new ApiTestHelper();

        private Cart cart;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var cartID = await SetupData();
            cart = await apiHelper.GetCartByID(cartID);
        }

        [Test]
        public void cart_should_reflect_correct_coupon_values()
        {
            cart.CouponDiscount.Should().Be(10m);
            cart.TotalAmount.Should().Be(35m);
            cart.TotalAmountAfterCampaign.Should().Be(35m);
            cart.TotalAmountAfterDiscounts.Should().Be(25m);
            cart.AppliedCoupon.Should().NotBeNull();
        }

        [Test]
        public void coupon_applied_line_item_should_have_correct_coupon_values()
        {
            var lineItem = cart.LineItems.Single(l => l.CouponDiscount != 0m);

            lineItem.CouponDiscount.Should().Be(10m);
            lineItem.TotalAmount.Should().Be(30m);
            lineItem.TotalAmountAfterCampaignDiscount.Should().Be(30m);
            lineItem.TotalAmountAfterDiscounts.Should().Be(20m);
            lineItem.TotalDiscount.Should().Be(10m);
        }

        [Test]
        public void coupon_not_applied_line_item_should_have_correct_coupon_values()
        {
            var lineItem = cart.LineItems.Single(l => l.CouponDiscount == 0m);

            lineItem.CouponDiscount.Should().Be(0m);
            lineItem.TotalAmount.Should().Be(5m);
            lineItem.TotalAmountAfterCampaignDiscount.Should().Be(5m);
            lineItem.TotalAmountAfterDiscounts.Should().Be(5m);
            lineItem.TotalDiscount.Should().Be(0m);
        }

        private async Task<Guid> SetupData()
        {
            var cartID = await apiHelper.CreateACart();
            var categoryID = await apiHelper.CreateACategory(new CreateCategoryRequest
            {
                Title = "category"
            });

            var productID1 = await apiHelper.CreateAProduct(new CreateProductRequest
            {
                CategoryID = categoryID,
                Price = 10m,
                Title = "Product1"
            });
            var productID2 = await apiHelper.CreateAProduct(new CreateProductRequest
            {
                CategoryID = categoryID,
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