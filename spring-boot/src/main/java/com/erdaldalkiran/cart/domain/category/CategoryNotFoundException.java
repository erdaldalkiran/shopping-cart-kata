package com.erdaldalkiran.cart.domain.category;

import java.util.UUID;

public class CategoryNotFoundException extends Exception {
    public CategoryNotFoundException(UUID id) {
        super(String.format("category %s not found", id.toString()));
    }
}
