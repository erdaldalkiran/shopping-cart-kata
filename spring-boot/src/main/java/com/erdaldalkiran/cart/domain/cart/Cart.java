package com.erdaldalkiran.cart.domain.cart;

import com.erdaldalkiran.cart.domain.campaign.Campaign;
import com.erdaldalkiran.cart.domain.coupon.Coupon;
import com.erdaldalkiran.cart.domain.product.Product;
import lombok.AccessLevel;
import lombok.Getter;

import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.util.*;

@Getter
public class Cart {
    private final UUID id;
    private Coupon appliedCoupon;
    private BigDecimal couponDiscount;
    private BigDecimal deliveryCost;

    @Getter(AccessLevel.NONE)
    private List<LineItem> lineItems;

    public Cart(UUID id) {
        this.id = id;
        lineItems = Collections.emptyList();
    }

    public Collection<LineItem> getLineItems() {
        return Collections.unmodifiableCollection(lineItems);
    }

    public boolean isEmpty() {
        return lineItems.isEmpty();
    }

    public int getLineItemsCount() {
        return lineItems.size();
    }

    public int getDistinctCategoriesCount() {
        return (int) lineItems.stream().map(l -> l.getProduct().getCategoryID()).distinct().count();
    }

    public BigDecimal getTotalAmount() {
        var amount = lineItems.stream().map(LineItem::getTotalAmount)
                .reduce(BigDecimal::add);
        if (amount.isEmpty()) return BigDecimal.ZERO;
        return amount.get().round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public BigDecimal getCampaignDiscount() {
        var amount = lineItems.stream().map(LineItem::getCampaignDiscount)
                .reduce(BigDecimal::add);
        if (amount.isEmpty()) return BigDecimal.ZERO;
        return amount.get().round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public BigDecimal getTotalAmountAfterDiscounts() {
        return getTotalAmount().subtract(getCampaignDiscount()).subtract(getCouponDiscount()).round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public BigDecimal getTotalAmountAfterCampaign() {
        return getTotalAmount().subtract(getCampaignDiscount()).round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public void addItem(Product product, int quantity) {
        if (product == null) throw new NullPointerException("product cannot  be null.");
        if (quantity < 1) throw new IllegalArgumentException("quantity must be greater than 0.");
        var optionalLineItem = lineItems.stream().filter(l -> l.getProduct().getId().equals(product.getId())).findFirst();
        if (optionalLineItem.isEmpty()) {
            lineItems.add(new LineItem(product, quantity));
            return;
        }

        //ASSUMPTION: last added product has the most current information
        lineItems.remove(optionalLineItem.get());
        var totalQuantity = quantity + optionalLineItem.get().getQuantity();
        lineItems.add(new LineItem(product, totalQuantity));
    }

    public void applyCampaign(Campaign campaign) {
        if (campaign == null) throw new NullPointerException("campaign cannot  be null.");

        clearCampaign();
        var isApplicable = campaign.isApplicableTo(this);
        if (!isApplicable) return;

        lineItems.forEach(l -> l.applyCampaign(campaign));
    }

    public void clearCampaign() {
        lineItems.forEach(LineItem::clearCampaign);
    }

    public void setDeliveryCost(BigDecimal cost) {
        if (cost.compareTo(BigDecimal.ZERO) < 1) throw new IllegalArgumentException("cost must be greater than 0.");

        deliveryCost = cost.round(new MathContext(2, RoundingMode.HALF_EVEN));
    }

    public void applyCoupon(Coupon coupon) {
        if (coupon == null) throw new NullPointerException("coupon cannot be null.");

        clearCoupon();

        var isApplicable = coupon.isApplicableTo(this);
        if (!isApplicable) return;

        appliedCoupon = coupon;
        couponDiscount = coupon.calculateDiscountAmountFor(this).get();
        distributeCouponDiscountToLineItems();
    }

    private void clearCoupon() {
        appliedCoupon = null;
        couponDiscount = BigDecimal.ZERO;
        lineItems.forEach(LineItem::clearCouponDiscount);
    }

    private void distributeCouponDiscountToLineItems() {
        var remainingAmount = couponDiscount;
        for (var lineItem : lineItems) {
            if (lineItem.getTotalAmountAfterCampaignDiscount().compareTo(remainingAmount) > -1) {
                lineItem.setCouponDiscount(remainingAmount);
                break;
            }

            remainingAmount = remainingAmount.subtract(lineItem.getTotalAmountAfterCampaignDiscount());
            lineItem.setCouponDiscount(lineItem.getTotalAmountAfterCampaignDiscount());
        }
    }
}
