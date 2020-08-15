using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_adding_lineitems_to_cart
{
    internal class And_line_item_does_not_exists
    {
        private const int quantity = 3;
        private readonly Cart cart = new Cart();

        private readonly Product product =
            new Product(Guid.NewGuid(), "Title", new Price(100m, Currency.TL), Guid.NewGuid());

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            cart.AddItem(product, 3);
        }

        [Test]
        public void line_item_should_be_added()
        {
            var lineItems = cart.GetLineItems();

            var retrievedLineItem = lineItems.Single(l => l.Product.Id == product.Id);

            retrievedLineItem.Product.Should().BeEquivalentTo(product);
            retrievedLineItem.Quantity.Should().Be(quantity);
        }

        [Test]
        public void cart_total_amount_should_be_updated()
        {
            var expectedAmount = product.Price * quantity;

            cart.TotalAmount().Should().Be(expectedAmount);
        }
    }
}