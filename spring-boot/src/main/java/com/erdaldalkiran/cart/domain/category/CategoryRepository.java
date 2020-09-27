package com.erdaldalkiran.cart.domain.category;

import org.springframework.stereotype.Repository;

import java.util.concurrent.Future;

public interface CategoryRepository {
    Future<Void> add(Category category);
}
