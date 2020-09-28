package com.erdaldalkiran.cart.domain.product;

import java.util.Collection;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.Future;

public interface ProductReader {
    Future<List<Product>> getByIDs(Collection<UUID> ids);
    Future<List<Product>> getAll();
}
