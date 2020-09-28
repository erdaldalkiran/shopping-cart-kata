package com.erdaldalkiran.cart.domain.coupon;

import java.util.Collection;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.Future;

public interface CouponReader {
    Future<List<Coupon>> getByIDs(Collection<UUID> ids);
    Future<List<Coupon>> getAll();
}
