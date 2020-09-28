package com.erdaldalkiran.cart.infrastructure.product;

import com.erdaldalkiran.cart.domain.product.Product;
import com.erdaldalkiran.cart.domain.product.ProductRepository;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

@Repository
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class InMemoryProductRepository implements ProductRepository {

    private final List<Product> products;

    public InMemoryProductRepository(InMemoryProductDB db) {
        this.products = db.getDB();
    }

    @Override
    public Future<Void> add(Product category) {
        products.add(category);

        return new CompletableFuture<>();
    }
}
