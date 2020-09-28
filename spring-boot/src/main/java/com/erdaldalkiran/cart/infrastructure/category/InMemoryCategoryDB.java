package com.erdaldalkiran.cart.infrastructure.category;

import com.erdaldalkiran.cart.domain.category.Category;
import org.springframework.beans.factory.annotation.Configurable;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_SINGLETON)
public class InMemoryCategoryDB {
    private final List<Category> db = new ArrayList<>();
    List<Category> getDB(){
        return db;
    }
}
