package com.erdaldalkiran.cart.infrastructure.campaign;

import com.erdaldalkiran.cart.domain.campaign.Campaign;
import com.erdaldalkiran.cart.domain.campaign.CampaignRepository;
import com.erdaldalkiran.cart.infrastructure.campaign.InMemoryCampaignDB;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.Future;

@Repository
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class InMemoryCampaignRepository implements CampaignRepository {

    private final List<Campaign> campaigns;

    public InMemoryCampaignRepository(InMemoryCampaignDB db) {
        this.campaigns = db.getDB();
    }

    @Override
    public Future<Void> add(Campaign category) {
        campaigns.add(category);

        return new CompletableFuture<>();
    }
}
