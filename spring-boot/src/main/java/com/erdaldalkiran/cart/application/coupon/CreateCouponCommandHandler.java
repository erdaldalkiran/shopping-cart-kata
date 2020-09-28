package com.erdaldalkiran.cart.application.coupon;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Voidy;
import com.erdaldalkiran.cart.domain.coupon.Coupon;
import com.erdaldalkiran.cart.domain.coupon.CouponReader;
import com.erdaldalkiran.cart.domain.coupon.CouponRepository;
import lombok.SneakyThrows;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.Collections;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class CreateCouponCommandHandler implements Command.Handler<CreateCouponCommand, CompletableFuture<Voidy>> {

    private final CouponRepository repository;

    public CreateCouponCommandHandler(CouponRepository repository) {
        this.repository = repository;
    }

    @SneakyThrows
    @Override
    public CompletableFuture<Voidy> handle(CreateCouponCommand command) {
        var coupon = new Coupon(command.getId(), command.getMinimumCartAmount(), command.getType(), command.getRate());
        repository.add(coupon);

        return CompletableFuture.completedFuture(new Voidy());
    }
}
