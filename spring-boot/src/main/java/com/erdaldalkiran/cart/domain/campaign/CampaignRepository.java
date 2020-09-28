package com.erdaldalkiran.cart.domain.campaign;

import java.util.concurrent.Future;

public interface CampaignRepository {
    Future<Void> add(Campaign category);
}
