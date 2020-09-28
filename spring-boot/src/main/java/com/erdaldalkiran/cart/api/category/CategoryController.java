package com.erdaldalkiran.cart.api.category;

import an.awesome.pipelinr.Pipeline;
import com.erdaldalkiran.cart.application.category.CreateCategoryCommand;
import com.erdaldalkiran.cart.domain.category.Category;
import com.erdaldalkiran.cart.domain.category.CategoryReader;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.net.URI;
import java.util.Collection;
import java.util.Collections;
import java.util.Optional;
import java.util.UUID;
import java.util.concurrent.ExecutionException;

@RestController
@RequestMapping("/category")
public class CategoryController {
    private final Pipeline pipeline;
    private final CategoryReader reader;

    public CategoryController(Pipeline pipeline, CategoryReader reader) {
        this.pipeline = pipeline;
        this.reader = reader;
    }

    @GetMapping("/id/{id}")
    public ResponseEntity<Category> getByID(@PathVariable UUID id) throws ExecutionException, InterruptedException {
        var category = reader.getByIDs(Collections.singletonList(id)).get();
        if (category.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(category.get(0));
    }

    @GetMapping("/all")
    public ResponseEntity<Collection<Category>> getAll() throws ExecutionException, InterruptedException {
        var categories = reader.getAll().get();
        if (categories.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(categories);
    }

    @PostMapping()
    public ResponseEntity<Void> create(@Valid @RequestBody CreateCategoryRequest request) {
        final var id = UUID.randomUUID();
        var command = new CreateCategoryCommand(id, Optional.ofNullable(request.getParentID()) , request.getTitle());
        command.execute(pipeline);

        return ResponseEntity.created(URI.create("/category/id/" + id)).build();
    }
}
