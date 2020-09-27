package com.erdaldalkiran.cart.api.category;

import lombok.Getter;
import lombok.Setter;

import java.util.Optional;
import java.util.UUID;

@Getter
@Setter
public class CreateCategoryRequest {
    private UUID parentID;
    private String Title;
}
