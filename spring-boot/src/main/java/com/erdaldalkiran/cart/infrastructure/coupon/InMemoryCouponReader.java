package com.erdaldalkiran.cart.infrastructure.coupon;

import com.erdaldalkiran.cart.domain.coupon.Coupon;
import com.erdaldalkiran.cart.domain.coupon.CouponReader;
import com.erdaldalkiran.cart.infrastructure.coupon.InMemoryCouponDB;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

import static java.util.stream.Collectors.toUnmodifiableList;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class InMemoryCouponReader implements CouponReader {

    private final List<Coupon> coupons;

    public InMemoryCouponReader(InMemoryCouponDB db) {
        this.coupons = db.getDB();
    }

    @Override
    public Future<List<Coupon>> getByIDs(final Collection<UUID> ids) {
        var result = coupons.stream().filter(c -> ids.contains(c.getId())).collect(toUnmodifiableList());

        return CompletableFuture.completedFuture(result);
    }

    @Override
    public Future<List<Coupon>> getAll() {
        var result = Collections.unmodifiableList(coupons);

        return CompletableFuture.completedFuture(result);
    }
}
