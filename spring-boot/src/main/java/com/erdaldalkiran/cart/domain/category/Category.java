package com.erdaldalkiran.cart.domain.category;

import lombok.Getter;

import java.util.Optional;
import java.util.UUID;

@Getter
public class Category {
    private final UUID id;
    private final Optional<UUID> parentID;
    private final String title;

    public Category(UUID id, Optional<UUID> parentID, String title) {
        this.id = id;
        this.parentID = parentID;
        this.title = title;
    }
}
