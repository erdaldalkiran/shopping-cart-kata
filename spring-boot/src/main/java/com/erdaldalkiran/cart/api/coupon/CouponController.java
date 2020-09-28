package com.erdaldalkiran.cart.api.coupon;

import an.awesome.pipelinr.Pipeline;
import com.erdaldalkiran.cart.application.coupon.CreateCouponCommand;
import com.erdaldalkiran.cart.domain.coupon.Coupon;
import com.erdaldalkiran.cart.domain.coupon.CouponReader;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.net.URI;
import java.util.Collection;
import java.util.Collections;
import java.util.UUID;
import java.util.concurrent.ExecutionException;

@RestController
@RequestMapping("/coupon")
public class CouponController {
    private final Pipeline pipeline;
    private final CouponReader reader;

    public CouponController(Pipeline pipeline, CouponReader reader) {
        this.pipeline = pipeline;
        this.reader = reader;
    }

    @GetMapping("/id/{id}")
    public ResponseEntity<Coupon> getByID(@PathVariable UUID id) throws ExecutionException, InterruptedException {
        var coupon = reader.getByIDs(Collections.singletonList(id)).get();
        if (coupon.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(coupon.get(0));
    }

    @GetMapping("/all")
    public ResponseEntity<Collection<Coupon>> getAll() throws ExecutionException, InterruptedException {
        var coupons = reader.getAll().get();
        if (coupons.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(coupons);
    }

    @PostMapping()
    public ResponseEntity<Void> create(@Valid @RequestBody CreateCouponRequest request) {
        final var id = UUID.randomUUID();
        var command = new CreateCouponCommand(id, request.getMinimumCartAmount(), request.getDiscountType(), request.getRate());
        command.execute(pipeline);

        return ResponseEntity.created(URI.create("/coupon/id/" + id)).build();
    }
}
