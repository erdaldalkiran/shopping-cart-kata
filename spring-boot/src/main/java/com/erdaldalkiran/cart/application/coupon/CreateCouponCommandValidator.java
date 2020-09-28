package com.erdaldalkiran.cart.application.coupon;

import com.erdaldalkiran.cart.domain.coupon.Coupon;
import com.erdaldalkiran.cart.domain.discount.DiscountType;

import javax.validation.ConstraintValidator;
import javax.validation.ConstraintValidatorContext;
import java.math.BigDecimal;

public class CreateCouponCommandValidator implements ConstraintValidator<CreateCouponCommandValidation, CreateCouponCommand> {
    public void initialize(CreateCouponCommandValidation constraint) {
    }

    @Override
    public boolean isValid(CreateCouponCommand command, ConstraintValidatorContext context) {
        if(command.getType().equals(DiscountType.Rate) && command.getRate().compareTo(BigDecimal.ONE) == 1){
            return false;
        }
        return true;
    }

}
