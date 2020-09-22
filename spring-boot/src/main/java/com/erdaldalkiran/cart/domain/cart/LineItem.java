package com.erdaldalkiran.cart.domain.cart;

import com.erdaldalkiran.cart.domain.campaign.Campaign;
import com.erdaldalkiran.cart.domain.product.Product;
import lombok.Getter;

import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;

@Getter
public class LineItem {
    private final Product product;
    private final int quantity;
    private Campaign appliedCampaign;
    private BigDecimal couponDiscount;

    public LineItem(Product product, int quantity) {
        this.product = product;
        this.quantity = quantity;
    }

    public BigDecimal getCampaignDiscount() {
        if (appliedCampaign == null) {
            return BigDecimal.ZERO;
        }

        return appliedCampaign.calculateDiscountAmountFor(this)
                .orElse(BigDecimal.ZERO);
    }

    public BigDecimal getTotalDiscount() {
        return getCampaignDiscount().add(couponDiscount).round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public BigDecimal getTotalAmount() {
        return product.getPrice().multiply(BigDecimal.valueOf(quantity)).round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public BigDecimal getTotalAmountAfterCampaignDiscount() {
        return getTotalAmount().subtract(getCampaignDiscount()).round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public BigDecimal getTotalAmountAfterDiscounts() {
        return getTotalAmount().subtract(getTotalDiscount()).round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public void clearCampaign() {
        appliedCampaign = null;
    }

    public void applyCampaign(Campaign campaign) {
        var isApplicable = campaign.isApplicableTo(this);
        if (!isApplicable) return;
        appliedCampaign = campaign;
    }

    public void clearCouponDiscount() {
        couponDiscount = BigDecimal.ZERO;
    }

    public void setCouponDiscount(BigDecimal amount) {
        couponDiscount = amount.round(new MathContext(2, RoundingMode.HALF_EVEN));
    }
}
