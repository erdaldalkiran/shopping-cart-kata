package com.erdaldalkiran.cart.infrastructure.product;

import com.erdaldalkiran.cart.domain.product.Product;
import com.erdaldalkiran.cart.domain.product.ProductReader;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

import static java.util.stream.Collectors.toUnmodifiableList;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class InMemoryProductReader implements ProductReader {

    private final List<Product> products;

    public InMemoryProductReader(InMemoryProductDB db) {
        this.products = db.getDB();
    }

    @Override
    public Future<List<Product>> getByIDs(final Collection<UUID> ids) {
        var result = products.stream().filter(c -> ids.contains(c.getId())).collect(toUnmodifiableList());

        return CompletableFuture.completedFuture(result);
    }

    @Override
    public Future<List<Product>> getAll() {
        var result = Collections.unmodifiableList(products);

        return CompletableFuture.completedFuture(result);
    }
}
