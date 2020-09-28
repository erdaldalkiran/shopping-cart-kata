package com.erdaldalkiran.cart.application.campaign;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Voidy;
import com.erdaldalkiran.cart.application.discount.OnlyValidDiscountTypes;
import com.erdaldalkiran.cart.domain.discount.DiscountType;
import lombok.Getter;

import javax.validation.constraints.NotBlank;
import javax.validation.constraints.NotNull;
import javax.validation.constraints.Positive;
import java.math.BigDecimal;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;

@Getter
@CreateCampaignCommandValidation
public class CreateCampaignCommand implements Command<CompletableFuture<Voidy>> {

    @NotNull
    private final UUID id;

    @NotNull
    private final UUID categoryID;

    @Positive
    private final int minimumItemCount;

    @NotNull
    @OnlyValidDiscountTypes
    private DiscountType discountType;

    @NotNull
    @Positive
    private final BigDecimal rate;

    public CreateCampaignCommand(@NotNull UUID id,
                                 @NotNull UUID categoryID,
                                 @NotNull int minimumItemCount,
                                 @NotNull DiscountType discountType,
                                 @NotNull @Positive BigDecimal rate) {
        this.id = id;
        this.categoryID = categoryID;
        this.minimumItemCount = minimumItemCount;
        this.discountType = discountType;
        this.rate = rate;
    }
}
