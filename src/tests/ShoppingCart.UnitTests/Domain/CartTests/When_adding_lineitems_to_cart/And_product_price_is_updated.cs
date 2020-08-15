using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_adding_lineitems_to_cart
{
    internal class And_product_price_is_updated
    {
        private const int initialQuantity = 2;

        private const int quantity = 5;
        private static readonly Guid productId = Guid.NewGuid();
        private static readonly Guid categoryId = Guid.NewGuid();

        private readonly Cart cart = new Cart();

        private readonly Product initialProduct =
            new Product(productId, "Title", new Price(100m, Currency.TL), categoryId);

        private readonly Product updatedProduct =
            new Product(productId, "Title", new Price(120m, Currency.TL), categoryId);

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

            var lineItems = cart.GetLineItems();
            var retrievedLineItem = lineItems.Single(l => l.Product.Id == productId);

            retrievedLineItem.Quantity.Should().Be(expectedQuantity);
        }

        [Test]
        public void cart_total_amount_should_be_updated()
        {
            var latestPrice = updatedProduct.Price;
            var totalQuantity = initialQuantity + quantity;
            var expectedAmount = latestPrice * totalQuantity;

            cart.TotalAmount().Should().Be(expectedAmount);
        }

        private void SetupCart()
        {
            cart.AddItem(initialProduct, initialQuantity);
        }
    }
}