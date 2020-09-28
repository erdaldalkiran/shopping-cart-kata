package com.erdaldalkiran.cart.application.category;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Voidy;
import lombok.Getter;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;
import java.util.Optional;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;

@Getter
public class CreateCategoryCommand implements Command<CompletableFuture<Voidy>> {

    @NotNull
    private final UUID id;

    @NotNull
    private final Optional<UUID> parentCategoryID;

    @NotBlank
    private final String title;

    public CreateCategoryCommand(@NotNull UUID id,
                                 @NotNull Optional<UUID> parentCategoryID,
                                 @NotBlank String title) {
        this.id = id;
        this.parentCategoryID = parentCategoryID;
        this.title = title;
    }
}
