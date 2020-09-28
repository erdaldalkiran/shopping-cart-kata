package com.erdaldalkiran.cart.domain.product;

import com.erdaldalkiran.cart.domain.category.Category;

import java.util.concurrent.Future;

public interface ProductRepository {
    Future<Void> add(Product product);
}
