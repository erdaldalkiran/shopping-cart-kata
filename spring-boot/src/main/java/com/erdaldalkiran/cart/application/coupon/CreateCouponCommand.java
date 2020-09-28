package com.erdaldalkiran.cart.application.coupon;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Voidy;
import com.erdaldalkiran.cart.application.discount.OnlyValidDiscountTypes;
import com.erdaldalkiran.cart.domain.discount.DiscountType;
import lombok.Getter;

import javax.validation.constraints.NotNull;
import javax.validation.constraints.Positive;
import java.math.BigDecimal;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;

@Getter
@CreateCouponCommandValidation
public class CreateCouponCommand implements Command<CompletableFuture<Voidy>> {

    @NotNull
    private final UUID id;

    @NotNull
    @Positive
    private BigDecimal minimumCartAmount;

    @NotNull
    @OnlyValidDiscountTypes
    private DiscountType type;

    @NotNull
    @Positive
    private BigDecimal rate;

    public CreateCouponCommand(@NotNull UUID id,
                               @NotNull @Positive BigDecimal minimumCartAmount,
                               @NotNull DiscountType type,
                               @NotNull @Positive BigDecimal rate) {
        this.id = id;
        this.minimumCartAmount = minimumCartAmount;
        this.type = type;
        this.rate = rate;
    }
}
