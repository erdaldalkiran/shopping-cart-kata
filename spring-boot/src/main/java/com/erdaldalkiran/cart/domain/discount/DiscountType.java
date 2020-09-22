package com.erdaldalkiran.cart.domain.discount;

public enum DiscountType {
    Rate(1,"rate"), Amount(2,"amount");

    private int value;
    private String name;

    DiscountType(int value, String name ) {
        this.value = value;
        this. name = name;
    }
}
