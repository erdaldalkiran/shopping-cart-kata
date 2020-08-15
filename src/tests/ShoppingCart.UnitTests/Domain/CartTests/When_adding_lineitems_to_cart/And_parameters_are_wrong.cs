using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Domain;

namespace ShoppingCart.UnitTests.Domain.CartTests.When_adding_lineitems_to_cart
{
    internal class And_parameters_are_wrong
    {
        [Test]
        public void it_should_throw_ArgumentNullException_when_product_is_null()
        {
            var cart = new Cart();

            Action action = () => cart.AddItem(null, 3);

            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void it_should_throw_ArgumentException_when_quantity_is_less_than_one()
        {
            var cart = new Cart();
            var product = new Product(Guid.NewGuid(), "Title", new Price(100m, Currency.TL), Guid.NewGuid());

            Action action = () => cart.AddItem(product, 0);

            action.Should().Throw<ArgumentException>();
        }
    }
}