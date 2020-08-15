using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_adding_line_items
{
    internal class And_line_item_does_not_exists
    {
        private const int quantity = 3;
        private readonly Cart cart = new Cart(Guid.NewGuid());

        private readonly Product product =
            new Product(Guid.NewGuid(), "Title", 100m, Guid.NewGuid());

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            cart.AddItem(product, 3);
        }

        [Test]
        public void line_item_should_be_added()
        {
            var lineItems = cart.GetLineItems();

            var retrievedLineItem = lineItems.Single(l => l.Product.ID == product.ID);

            retrievedLineItem.Product.Should().BeEquivalentTo(product);
            retrievedLineItem.Quantity.Should().Be(quantity);
        }

        [Test]
        public void cart_total_amount_should_be_updated()
        {
            var expectedAmount = product.Price * quantity;

            cart.TotalAmount.Should().Be(expectedAmount);
        }
    }
}