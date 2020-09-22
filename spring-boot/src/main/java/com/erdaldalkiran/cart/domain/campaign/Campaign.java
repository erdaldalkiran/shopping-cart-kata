package com.erdaldalkiran.cart.domain.campaign;

import com.erdaldalkiran.cart.domain.cart.Cart;
import com.erdaldalkiran.cart.domain.cart.LineItem;
import com.erdaldalkiran.cart.domain.discount.DiscountType;
import lombok.Getter;

import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.util.*;
import java.util.function.BiFunction;
import java.util.stream.Collectors;

@Getter
public class Campaign {

    private final UUID id;
    private final UUID categoryID;
    private final int minimumItemCount;
    private final DiscountType discountType;
    private final BigDecimal rate;

    public Campaign(UUID id, UUID categoryID, int minimumItemCount, DiscountType discountType, BigDecimal rate) {
        this.id = id;
        this.categoryID = categoryID;
        this.minimumItemCount = minimumItemCount;
        this.discountType = discountType;
        this.rate = rate;
    }

    public Optional<BigDecimal> calculateDiscountAmountFor(Cart cart) {
        var isApplicable = isApplicableTo(cart);
        if (!isApplicable) return Optional.empty();

        var items = getCampaignApplicableLineItems(cart);
        return items.stream()
                .map(l -> CampaignDiscountAmountCalculator.Strategies.get(discountType).apply(l, rate))
                .reduce(BigDecimal::add);
    }

    public Optional<BigDecimal> calculateDiscountAmountFor(LineItem lineItem) {
        var isApplicable = isApplicableTo(lineItem);
        if (!isApplicable) return Optional.empty();

        var discountAmount = CampaignDiscountAmountCalculator.Strategies.get(this.discountType).apply(lineItem, this.rate);
        return Optional.of(discountAmount);
    }

    public boolean isApplicableTo(Cart cart) {
        var items = getCampaignApplicableLineItems(cart);

        return doesCartContainMinimumItemCount(items);
    }

    public boolean isApplicableTo(LineItem lineItem) {
        var categoryRequirement = lineItem.getProduct().getCategoryID() == this.categoryID;
        var priceRequirement = true;
        if (this.discountType == DiscountType.Amount)
            priceRequirement = lineItem.getProduct().getPrice().compareTo(this.rate) > 0;

        return categoryRequirement && priceRequirement;
    }

    private List<LineItem> getCampaignApplicableLineItems(Cart cart) {
        return cart.getLineItems().stream().filter(this::isApplicableTo).collect(Collectors.toList());
    }

    private boolean doesCartContainMinimumItemCount(List<LineItem> items) {
        var itemCount = items.stream().mapToInt(LineItem::getQuantity).sum();
        return itemCount > minimumItemCount;
    }

    public static class CampaignDiscountAmountCalculator {
        public static Map<DiscountType, BiFunction<LineItem, BigDecimal, BigDecimal>> Strategies = Map.of(
                DiscountType.Amount, (lineItem, rate) -> {
                    var quantity = BigDecimal.valueOf(lineItem.getQuantity());
                    var discountAmount = quantity.multiply(rate);

                    return discountAmount.round(new MathContext(2, RoundingMode.HALF_EVEN));
                },
                DiscountType.Rate, (lineItem, rate) -> {
                    var price = lineItem.getProduct().getPrice();
                    var quantity = BigDecimal.valueOf(lineItem.getQuantity());
                    var discountAmount = price.multiply(quantity).multiply(rate);

                    return discountAmount.round(new MathContext(2, RoundingMode.HALF_EVEN));
                }
        );
    }
}

