package com.erdaldalkiran.cart.application.discount;

import com.erdaldalkiran.cart.domain.discount.DiscountType;

import javax.validation.ConstraintValidator;
import javax.validation.ConstraintValidatorContext;
import java.util.Arrays;
import java.util.List;

public class DiscountTypeSubSetValidator implements ConstraintValidator<OnlyValidDiscountTypes, DiscountType> {
    private List<DiscountType> subset;

    @Override
    public void initialize(OnlyValidDiscountTypes constraint) {

        this.subset = Arrays.asList(constraint.anyOf());
    }

    @Override
    public boolean isValid(DiscountType value, ConstraintValidatorContext context) {
        return value == null || subset.contains(value);
    }
}
