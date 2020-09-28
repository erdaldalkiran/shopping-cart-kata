package com.erdaldalkiran.cart.domain.coupon;

import java.util.concurrent.Future;

public interface CouponRepository {
    Future<Void> add(Coupon category);
}
