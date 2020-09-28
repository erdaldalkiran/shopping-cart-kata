package com.erdaldalkiran.cart.infrastructure.campaign;

import com.erdaldalkiran.cart.domain.campaign.Campaign;
import com.erdaldalkiran.cart.domain.campaign.CampaignReader;
import com.erdaldalkiran.cart.infrastructure.campaign.InMemoryCampaignDB;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.Collection;
import java.util.Collections;
import java.util.List;
import java.util.UUID;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

import static java.util.stream.Collectors.toUnmodifiableList;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class InMemoryCampaignReader implements CampaignReader {

    private final List<Campaign> campaigns;

    public InMemoryCampaignReader(InMemoryCampaignDB db) {
        this.campaigns = db.getDB();
    }

    @Override
    public Future<List<Campaign>> getByCategories(Collection<UUID> categoryIDs) {
        var result = campaigns.stream().filter(c -> categoryIDs.contains(c.getCategoryID())).collect(toUnmodifiableList());

        return CompletableFuture.completedFuture(result);
    }

    @Override
    public Future<List<Campaign>> getByIDs(final Collection<UUID> ids) {
        var result = campaigns.stream().filter(c -> ids.contains(c.getId())).collect(toUnmodifiableList());

        return CompletableFuture.completedFuture(result);
    }

    @Override
    public Future<List<Campaign>> getAll() {
        var result = Collections.unmodifiableList(campaigns);

        return CompletableFuture.completedFuture(result);
    }
}
