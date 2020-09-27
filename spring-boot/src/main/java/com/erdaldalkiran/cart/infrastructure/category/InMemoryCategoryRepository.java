package com.erdaldalkiran.cart.infrastructure.category;

import com.erdaldalkiran.cart.domain.category.Category;
import com.erdaldalkiran.cart.domain.category.CategoryRepository;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

@Repository
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class InMemoryCategoryRepository implements CategoryRepository {

    private final List<Category> categories;

    public InMemoryCategoryRepository(InMemoryDB db) {
        this.categories = db.getDB();
    }

    @Override
    public Future<Void> add(Category category) {
        categories.add(category);

        return new CompletableFuture<>();
    }
}
