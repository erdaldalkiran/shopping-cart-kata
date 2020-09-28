package com.erdaldalkiran.cart.application.product;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Voidy;
import com.erdaldalkiran.cart.domain.category.CategoryNotFoundException;
import com.erdaldalkiran.cart.domain.category.CategoryReader;
import com.erdaldalkiran.cart.domain.product.Product;
import com.erdaldalkiran.cart.domain.product.ProductRepository;
import lombok.Getter;
import lombok.SneakyThrows;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.Collections;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class CreateProductCommandHandler implements Command.Handler<CreateProductCommand, CompletableFuture<Voidy>> {

    private final ProductRepository repository;
    private final CategoryReader categoryReader;

    public CreateProductCommandHandler(ProductRepository repository, CategoryReader categoryReader) {
        this.repository = repository;
        this.categoryReader = categoryReader;
    }

    @SneakyThrows
    @Override
    public CompletableFuture<Voidy> handle(CreateProductCommand command) {
        ensureCategoryExists(command);

        var product = new Product(command.getId(), command.getTitle(), command.getPrice(), command.getCategoryID());
        repository.add(product);

        return CompletableFuture.completedFuture(new Voidy());
    }

    private void ensureCategoryExists(CreateProductCommand command) throws InterruptedException, ExecutionException, CategoryNotFoundException {
        var category = categoryReader.getByIDs(Collections.singletonList(command.getCategoryID())).get();
        if (category.isEmpty()) {
            throw new CategoryNotFoundException(command.getCategoryID());
        }
    }
}
