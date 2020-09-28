package com.erdaldalkiran.cart.api.product;

import an.awesome.pipelinr.Pipeline;
import com.erdaldalkiran.cart.application.product.CreateProductCommand;
import com.erdaldalkiran.cart.domain.product.Product;
import com.erdaldalkiran.cart.domain.product.ProductReader;
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
@RequestMapping("/product")
public class ProductController {
    private final Pipeline pipeline;
    private final ProductReader reader;

    public ProductController(Pipeline pipeline, ProductReader reader) {
        this.pipeline = pipeline;
        this.reader = reader;
    }

    @GetMapping("/id/{id}")
    public ResponseEntity<Product> getByID(@PathVariable UUID id) throws ExecutionException, InterruptedException {
        var product = reader.getByIDs(Collections.singletonList(id)).get();
        if (product.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(product.get(0));
    }

    @GetMapping("/all")
    public ResponseEntity<Collection<Product>> getAll() throws ExecutionException, InterruptedException {
        var products = reader.getAll().get();
        if (products.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(products);
    }

    @PostMapping()
    public ResponseEntity<Void> create(@Valid @RequestBody CreateProductRequest request) {
        final var id = UUID.randomUUID();
        var command = new CreateProductCommand(id, request.getTitle(), request.getPrice(), request.getCategoryID());
        command.execute(pipeline);

        return ResponseEntity.created(URI.create("/product/id/" + id)).build();
    }
}
