package com.erdaldalkiran.cart.domain.category;

import java.util.Collection;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.Future;

public interface CategoryReader {
    Future<List<Category>> getByIDs(Collection<UUID> ids);
    Future<List<Category>> getAll();
}
