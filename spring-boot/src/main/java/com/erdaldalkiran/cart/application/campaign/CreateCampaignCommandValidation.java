package com.erdaldalkiran.cart.application.campaign;

import javax.validation.Constraint;
import javax.validation.Payload;
import java.lang.annotation.*;

@Constraint(validatedBy = CreateCampaignCommandValidator.class)
@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
@Documented
public @interface CreateCampaignCommandValidation {
    String message() default "discount rate must be between 0 and 1 for rate type discounts";
    Class<?>[] groups() default { };
    Class<? extends Payload>[] payload() default { };
}
