package com.erdaldalkiran.cart.api.coupon;

import com.erdaldalkiran.cart.application.discount.OnlyValidDiscountTypes;
import com.erdaldalkiran.cart.domain.discount.DiscountType;
import lombok.Getter;
import lombok.Setter;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Positive;
import java.math.BigDecimal;
import java.util.UUID;

@Getter
@Setter
public class CreateCouponRequest {
    @NotNull
    @Positive
    private BigDecimal minimumCartAmount;

    @NotNull
    @OnlyValidDiscountTypes
    private DiscountType discountType;

    @NotNull
    @Positive
    private BigDecimal rate;

}
