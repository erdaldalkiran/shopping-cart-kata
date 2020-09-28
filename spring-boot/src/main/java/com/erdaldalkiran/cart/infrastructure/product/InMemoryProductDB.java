package com.erdaldalkiran.cart.infrastructure.product;

import com.erdaldalkiran.cart.domain.product.Product;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_SINGLETON)
public class InMemoryProductDB {
    private final List<Product> db = new ArrayList<>();
    List<Product> getDB(){
        return db;
    }
}
