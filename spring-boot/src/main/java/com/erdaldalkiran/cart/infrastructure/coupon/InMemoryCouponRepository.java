package com.erdaldalkiran.cart.infrastructure.coupon;

import com.erdaldalkiran.cart.domain.coupon.Coupon;
import com.erdaldalkiran.cart.domain.coupon.CouponRepository;
import com.erdaldalkiran.cart.infrastructure.coupon.InMemoryCouponDB;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

@Repository
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class InMemoryCouponRepository implements CouponRepository {

    private final List<Coupon> coupons;

    public InMemoryCouponRepository(InMemoryCouponDB db) {
        this.coupons = db.getDB();
    }

    @Override
    public Future<Void> add(Coupon coupon) {
        coupons.add(coupon);

        return new CompletableFuture<>();
    }
}
