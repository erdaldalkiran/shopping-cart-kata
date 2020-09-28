package com.erdaldalkiran.cart.application.product;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Voidy;
import lombok.Getter;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Positive;
import java.math.BigDecimal;
import java.util.Optional;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;

@Getter
public class CreateProductCommand implements Command<CompletableFuture<Voidy>> {

    @NotNull
    private final UUID id;

    @NotBlank
    private final String title;

    @NotNull
    @Positive
    private final BigDecimal price;

    @NotNull
    private final UUID categoryID;

    public CreateProductCommand(@NotNull UUID id,
                                @NotBlank String title,
                                @NotNull @Positive BigDecimal price,
                                @NotNull UUID categoryID) {
        this.id = id;
        this.title = title;
        this.price = price;
        this.categoryID = categoryID;
    }
}
