package com.erdaldalkiran.cart.application.campaign;

import com.erdaldalkiran.cart.domain.discount.DiscountType;

import javax.validation.ConstraintValidator;
import javax.validation.ConstraintValidatorContext;
import java.math.BigDecimal;

public class CreateCampaignCommandValidator implements ConstraintValidator<CreateCampaignCommandValidation, CreateCampaignCommand> {
    public void initialize(CreateCampaignCommandValidation constraint) {
    }

    @Override
    public boolean isValid(CreateCampaignCommand command, ConstraintValidatorContext context) {
        if(command.getDiscountType().equals(DiscountType.Rate) && command.getRate().compareTo(BigDecimal.ONE) == 1){
            return false;
        }
        return true;
    }

}
