package com.erdaldalkiran.cart.domain.discount;

import lombok.Getter;

@Getter
public enum DiscountType {
    None(0, "none"), Rate(1,"rate"), Amount(2,"amount");

    private final int value;
    private final String name;

    DiscountType(int value, String name ) {
        this.value = value;
        this. name = name;
    }
}
