package com.erdaldalkiran.cart.api.product;

import lombok.Getter;
import lombok.Setter;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Positive;
import javax.validation.constraints.Size;
import java.math.BigDecimal;
import java.util.UUID;

@Getter
@Setter
public class CreateProductRequest {
    @NotBlank
    private String title;

    @NotNull
    @Positive
    private BigDecimal price;

    @NotNull
    private UUID categoryID;

}
