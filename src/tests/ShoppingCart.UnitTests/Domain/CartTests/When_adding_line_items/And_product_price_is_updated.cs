using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_adding_line_items
{
    internal class And_product_price_is_updated
    {
        private const int initialQuantity = 2;

        private const int quantity = 5;
        private static readonly Guid productId = Guid.NewGuid();
        private static readonly Guid categoryId = Guid.NewGuid();

        private readonly Cart cart = new Cart(Guid.NewGuid());

        private readonly Product initialProduct =
            new Product(productId, "Title", 100m, categoryId);

        private readonly Product updatedProduct =
            new Product(productId, "Title", 120m, categoryId);

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupCart();
            cart.AddItem(updatedProduct, quantity);
        }

        [Test]
        public void line_item_quantity_should_be_updated()
        {
            var expectedQuantity = initialQuantity + quantity;

            var lineItems = cart.LineItems;
            var retrievedLineItem = lineItems.Single(l => l.Product.ID == productId);

            retrievedLineItem.Quantity.Should().Be(expectedQuantity);
        }

        [Test]
        public void cart_total_amount_should_be_updated()
        {
            var latestPrice = updatedProduct.Price;
            var totalQuantity = initialQuantity + quantity;
            var expectedAmount = latestPrice * totalQuantity;

            cart.TotalAmount.Should().Be(expectedAmount);
        }

        private void SetupCart()
        {
            cart.AddItem(initialProduct, initialQuantity);
        }
    }
}