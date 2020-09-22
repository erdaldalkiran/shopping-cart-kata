package com.erdaldalkiran.cart.domain.product;

import lombok.Getter;

import java.math.BigDecimal;
import java.util.UUID;

@Getter
public class Product {

    private final UUID id;
    private final String title;
    private final BigDecimal price;
    private final UUID categoryID;

    public Product(UUID id, String title, BigDecimal price, UUID categoryID) {
        this.id = id;
        this.title = title;
        this.price = price;
        this.categoryID = categoryID;
    }
}
