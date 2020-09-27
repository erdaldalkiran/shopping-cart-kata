package com.erdaldalkiran.cart.api;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;
@ComponentScan(basePackages ={
        "com.erdaldalkiran.cart.api",
        "com.erdaldalkiran.cart.application",
        "com.erdaldalkiran.cart.domain",
        "com.erdaldalkiran.cart.infrastructure",
} )
@SpringBootApplication

public class CartApiApplication {

    public static void main(String[] args) {
        SpringApplication.run(CartApiApplication.class, args);
    }

}
