package com.erdaldalkiran.cart.application.discount;

import com.erdaldalkiran.cart.domain.discount.DiscountType;

import javax.validation.Constraint;
import javax.validation.Payload;
import java.lang.annotation.Documented;
import java.lang.annotation.Retention;
import java.lang.annotation.Target;

import static java.lang.annotation.ElementType.*;
import static java.lang.annotation.RetentionPolicy.RUNTIME;

@Target({METHOD, FIELD, ANNOTATION_TYPE, CONSTRUCTOR, PARAMETER, TYPE_USE})
@Retention(RUNTIME)
@Documented
@Constraint(validatedBy = DiscountTypeSubSetValidator.class)
public @interface OnlyValidDiscountTypes {
    DiscountType[] anyOf() default {DiscountType.Amount, DiscountType.Rate};
    String message() default "must be any of {anyOf}";
    Class<?>[] groups() default {};
    Class<? extends Payload>[] payload() default {};
}

