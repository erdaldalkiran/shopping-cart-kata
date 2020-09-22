package com.erdaldalkiran.cart.domain.category;

import java.util.Optional;
import java.util.UUID;

public class Category {
    private UUID id;
    private Optional<UUID> parentID;
    private String title;

    public Category(UUID id, Optional<UUID> parentID, String title) {
        this.id = id;
        this.parentID = parentID;
        this.title = title;
    }
}
