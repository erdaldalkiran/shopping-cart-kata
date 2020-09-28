package com.erdaldalkiran.cart.domain.campaign;

import java.util.Collection;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.Future;

public interface CampaignReader {
    Future<List<Campaign>> getByCategories(Collection<UUID> categoryIDs);
    Future<List<Campaign>> getByIDs(Collection<UUID> ids);
    Future<List<Campaign>> getAll();
}
