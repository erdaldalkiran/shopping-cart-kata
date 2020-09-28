package com.erdaldalkiran.cart.api.category;

import lombok.Getter;
import lombok.Setter;

import javax.validation.constraints.NotBlank;
import java.util.UUID;

@Getter
@Setter
public class CreateCategoryRequest {
    private UUID parentID;

    @NotBlank
    private String Title;
}
