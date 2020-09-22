using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.API.Cart;
using ShoppingCart.API.Category;
using ShoppingCart.API.Product;
using ShoppingCart.Business.Cart;

namespace ShoppingCart.APITests.CartTests
{
    internal class When_adding_an_item_to_cart
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
        public void cart_should_reflect_correct_values()
        {
            cart.CouponDiscount.Should().Be(0m);
            cart.AppliedCoupon.Should().BeNull();
            cart.CampaignDiscount.Should().Be(0m);
            var expectedDeliveryCost = 7.99m;
            cart.DeliveryCost.Should().Be(expectedDeliveryCost);
            cart.LineItems.Should().HaveCount(1);
            cart.LineItems.First().Quantity.Should().Be(3);
            cart.LineItems.First().Product.Price.Should().Be(10m);
            cart.TotalAmount.Should().Be(30m);
            cart.TotalAmountAfterCampaign.Should().Be(30m);
            cart.TotalAmountAfterDiscounts.Should().Be(30m);
        }

        private async Task<Guid> SetupData()
        {
            var cartID = await apiHelper.CreateACart();
            var categoryID = await apiHelper.CreateACategory(new CreateCategoryRequest
            {
                Title = "category"
            });
            var productID = await apiHelper.CreateAProduct(new CreateProductRequest
            {
                CategoryID = categoryID,
                Price = 10m,
                Title = "Product"
            });
            await apiHelper.AddItemToTheCart(cartID, new AddItemRequest
            {
                ProductID = productID,
                Quantity = 3
            });
            return cartID;
        }
    }
}