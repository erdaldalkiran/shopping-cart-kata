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

    private final Optional<UUID> parentCategoryID;

    @NotBlank
    private final String title;

    public CreateCategoryCommand(UUID id, Optional<UUID> parentCategoryID, String title) {
        this.id = id;
        this.parentCategoryID = parentCategoryID;
        this.title = title;
    }

}
