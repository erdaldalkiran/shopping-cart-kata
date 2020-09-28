package com.erdaldalkiran.cart.infrastructure.coupon;

import com.erdaldalkiran.cart.domain.coupon.Coupon;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_SINGLETON)
public class InMemoryCouponDB {
    private final List<Coupon> db = new ArrayList<>();
    List<Coupon> getDB(){
        return db;
    }
}
