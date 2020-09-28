package com.erdaldalkiran.cart.application.category;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Voidy;
import com.erdaldalkiran.cart.domain.category.Category;
import com.erdaldalkiran.cart.domain.category.CategoryNotFoundException;
import com.erdaldalkiran.cart.domain.category.CategoryReader;
import com.erdaldalkiran.cart.domain.category.CategoryRepository;
import lombok.Getter;
import lombok.SneakyThrows;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.Collections;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class CreateCategoryCommandHandler implements Command.Handler<CreateCategoryCommand, CompletableFuture<Voidy>> {

    private final CategoryRepository repository;
    private final CategoryReader reader;

    public CreateCategoryCommandHandler(CategoryRepository repository, CategoryReader reader) {
        this.repository = repository;
        this.reader = reader;
    }

    @SneakyThrows
    @Override
    public CompletableFuture<Voidy> handle(CreateCategoryCommand command) {
        ensureParentCategoryExists(command);

        var category = new Category(command.getId(), command.getParentCategoryID(), command.getTitle());
        repository.add(category);

        return CompletableFuture.completedFuture(new Voidy());
    }

    private void ensureParentCategoryExists(CreateCategoryCommand command) throws InterruptedException, ExecutionException, CategoryNotFoundException {
        if (command.getParentCategoryID().isPresent()) {
            var parent = reader.getByIDs(Collections.singletonList(command.getParentCategoryID().get())).get();
            if(parent.isEmpty()){
                throw new CategoryNotFoundException(command.getParentCategoryID().get());
            }
        }
    }
}
