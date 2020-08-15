using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_adding_lineitems_to_cart
{
    internal class And_line_item_exists
    {
        private const int initialQuantity = 1;

        private const int quantity = 3;
        private readonly Cart cart = new Cart();

        private readonly Product product =
            new Product(Guid.NewGuid(), "Title", new Price(100m, Currency.TL), Guid.NewGuid());

        private Price initialTotalAmount;

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

            var lineItems = cart.GetLineItems();
            var retrievedLineItem = lineItems.Single(l => l.Product.Id == product.Id);

            retrievedLineItem.Quantity.Should().Be(expectedQuantity);
        }

        [Test]
        public void cart_total_amount_should_be_updated()
        {
            var expectedAmount = initialTotalAmount + product.Price * quantity;

            cart.TotalAmount().Should().Be(expectedAmount);
        }

        private void SetupCart()
        {
            cart.AddItem(product, initialQuantity);
            initialTotalAmount = cart.TotalAmount();
        }
    }
}