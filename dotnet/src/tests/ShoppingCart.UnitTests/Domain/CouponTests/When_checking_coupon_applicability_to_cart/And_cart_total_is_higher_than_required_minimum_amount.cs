﻿using System;
using FluentAssertions;
using NUnit.Framework;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Cart;
using ShoppingCart.Business.Coupon;
using ShoppingCart.Business.Product;

namespace ShoppingCart.UnitTests.Domain.CouponTests.When_checking_coupon_applicability_to_cart
{
    internal class And_cart_total_is_higher_than_required_minimum_amount
    {
        [Test]
        public void coupon_should_be_applicable()
        {
            var cart = new Cart(Guid.NewGuid());
            cart.AddItem(new Product(Guid.NewGuid(), "TitleA", 10m, Guid.NewGuid()), 2);

            var coupon = new Coupon(Guid.NewGuid(), 10m, DiscountType.Amount, 10m);

            var isApplicable = coupon.IsApplicableTo(cart);

            isApplicable.Should().BeTrue();
        }
    }
}