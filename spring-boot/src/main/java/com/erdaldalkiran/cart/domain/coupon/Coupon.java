package com.erdaldalkiran.cart.domain.coupon;

import com.erdaldalkiran.cart.domain.cart.Cart;
import com.erdaldalkiran.cart.domain.discount.DiscountType;
import lombok.Getter;

import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.util.Map;
import java.util.Optional;
import java.util.UUID;
import java.util.function.BiFunction;

@Getter
public class Coupon {
    private final UUID id;
    private final BigDecimal minimumCartAmount;
    private final DiscountType discountType;
    private final BigDecimal rate;


    public Coupon(UUID id, BigDecimal minimumCartAmount, DiscountType discountType, BigDecimal rate) {
        this.id = id;
        this.minimumCartAmount = minimumCartAmount;
        this.discountType = discountType;
        this.rate = rate;
    }

    public boolean isApplicableTo(Cart cart) {
        var amount = cart.getTotalAmountAfterCampaign();
        return amount.compareTo(minimumCartAmount) > 0;
    }

    public Optional<BigDecimal> calculateDiscountAmountFor(Cart cart) {
        var isApplicable = isApplicableTo(cart);
        if (!isApplicable) return Optional.empty();

        var cartAmount = cart.getTotalAmountAfterCampaign();
        var discountAmount = CouponDiscountAmountCalculator.Strategies
                .get(this.discountType).apply(cartAmount, this.rate);

        var  min = discountAmount.min(cartAmount);

        return Optional.of(min);
    }

    static class CouponDiscountAmountCalculator {
        static Map<DiscountType, BiFunction<BigDecimal,BigDecimal, BigDecimal>> Strategies = Map.of(
                DiscountType.Amount, (amount, rate) -> {
                    var discountAmount = rate;
                    return discountAmount.round(new MathContext(2, RoundingMode.HALF_EVEN));
                },
                DiscountType.Rate, (amount, rate) -> {
                    var discountAmount = amount.multiply(rate);
                    return discountAmount.round(new MathContext(2, RoundingMode.HALF_EVEN));
                }
        );
    }
}

