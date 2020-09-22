using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Delivery;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Services.DeliveryCostCalculatorTests
{
    [Description("cart has two products which belongs two different categories.")]
    internal class When_calculating_cost
    {
        [Test]
        public void delivery_cost_should_be_calculated_correctly()
        {
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 43m, Guid.NewGuid()), 1);
            cart.AddItem(new Product(Guid.NewGuid(), "TitleB", 17m, Guid.NewGuid()), 1);

            var calculator = new DeliveryCostCalculator(3m, 2m);

            var expectedCost = 12.99m;
            var cost = calculator.CalculateFor(cart);

            cost.Should().Be(expectedCost);
        }
    }
}