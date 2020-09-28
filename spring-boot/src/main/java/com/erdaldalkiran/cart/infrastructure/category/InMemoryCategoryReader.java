package com.erdaldalkiran.cart.infrastructure.category;

import com.erdaldalkiran.cart.domain.category.Category;
import com.erdaldalkiran.cart.domain.category.CategoryReader;
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
public class InMemoryCategoryReader implements CategoryReader {

    private final List<Category> categories;

    public InMemoryCategoryReader(InMemoryCategoryDB db) {
        this.categories = db.getDB();
    }

    @Override
    public Future<List<Category>> getByIDs(final Collection<UUID> ids) {
        var result = categories.stream().filter(c -> ids.contains(c.getId())).collect(toUnmodifiableList());

        return CompletableFuture.completedFuture(result);
    }

    @Override
    public Future<List<Category>> getAll() {
        var result = Collections.unmodifiableList(categories);

        return CompletableFuture.completedFuture(result);
    }
}
