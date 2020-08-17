using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_adding_line_items
{
    internal class And_line_item_exists
    {
        private const int initialQuantity = 1;

        private const int quantity = 3;
        private readonly Cart cart = new Cart(Guid.NewGuid());

        private readonly Product product =
            new Product(Guid.NewGuid(), "Title", 100m, Guid.NewGuid());

        private decimal initialTotalAmount;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupCart();
            cart.AddItem(product, quantity);
        }

        [Test]
        public void line_item_quantity_should_be_updated()
        {
            var expectedQuantity = initialQuantity + quantity;

            var retrievedLineItem =  cart.LineItems.Single(l => l.Product.ID == product.ID);

            retrievedLineItem.Quantity.Should().Be(expectedQuantity);
        }

        [Test]
        public void cart_total_amount_should_be_updated()
        {
            var expectedAmount = product.Price * (initialQuantity + quantity);

            cart.TotalAmount.Should().Be(expectedAmount);
        }

        private void SetupCart()
        {
            cart.AddItem(product, initialQuantity);
            initialTotalAmount = cart.TotalAmount;
        }
    }
}