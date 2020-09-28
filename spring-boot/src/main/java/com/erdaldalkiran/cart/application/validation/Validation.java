package com.erdaldalkiran.cart.application.validation;

import an.awesome.pipelinr.Command;
import org.springframework.core.annotation.Order;
import org.springframework.stereotype.Component;

import javax.validation.ConstraintViolationException;
import javax.validation.Validator;

@Component
@Order(1)
class Validation implements Command.Middleware {

    private final Validator validator;

    public Validation(Validator validator) {
        this.validator = validator;
    }

    @Override
    public <R, C extends Command<R>> R invoke(C command, Next<R> next) {
        var violations = validator.validate(command);
        if(!violations.isEmpty()){
            throw new ConstraintViolationException(violations);
        }

        R response = next.invoke();

        return response;
    }
}
