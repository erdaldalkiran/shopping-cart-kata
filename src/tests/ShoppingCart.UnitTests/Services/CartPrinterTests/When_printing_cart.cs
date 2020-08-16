using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingCart.Business.Domain;
using ShoppingCart.Business.Readers;
using ShoppingCart.Business.Services;

namespace ShoppingCart.UnitTests.Services.CartPrinterTests
{
    internal class When_printing_cart
    {
        private readonly Mock<ICategoryReader> categoryReader = new Mock<ICategoryReader>();
        private Cart cart;
        private CartPrinter printer;

        private readonly string expectedOutput = @"CategoryName ProductName Quantity UnitPrice TotalPrice TotalDiscount
Category 1 Title A 3 100TL 250TL 50TL
Category 2 Title B 2 40TL 80TL 0TL
Total Amount:330TL Delivery Cost: 32,99TL
";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupData();
        }

        private void SetupData()
        {
            var categoryID1 = Guid.Parse("BA38E701-024B-4968-ABED-510742481F0D");
            var category1 = new Category(categoryID1, Guid.NewGuid(), "Category 1");

            var categoryID2 = Guid.Parse("FD843460-7343-4C32-A2A8-493678AABA63");
            var category2 = new Category(categoryID2, Guid.NewGuid(), "Category 2");

            cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "Title A", 100m, categoryID1), 3);
            cart.AddItem(new Product(Guid.NewGuid(), "Title B", 40m, categoryID2), 2);

            var campaign = new Campaign(Guid.NewGuid(), categoryID1, 1, DiscountType.Amount, 10m);
            cart.ApplyCampaign(campaign);

            var coupon = new Coupon(Guid.NewGuid(), 20m, DiscountType.Amount, 20m);

            cart.ApplyCoupon(coupon);

            var deliveryCostCalculator = new DeliveryCostCalculator(10m, 5m);
            var deliveryCost = deliveryCostCalculator.CalculateFor(cart);
            cart.SetDeliveryCost(deliveryCost);

            categoryReader.Setup(r => r.GetByIDs(new List<Guid> {categoryID1, categoryID2}))
                .Returns(new List<Category> {category1, category2});

            printer = new CartPrinter(categoryReader.Object);
        }

        [Test]
        public void it_should_print_correctly()
        {
            var result = printer.Print(cart);
            result.Should().Be(expectedOutput);
        }
    }
}