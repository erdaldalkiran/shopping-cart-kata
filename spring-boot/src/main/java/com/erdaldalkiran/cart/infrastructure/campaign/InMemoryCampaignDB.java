package com.erdaldalkiran.cart.infrastructure.campaign;

import com.erdaldalkiran.cart.domain.campaign.Campaign;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_SINGLETON)
public class InMemoryCampaignDB {
    private final List<Campaign> db = new ArrayList<>();
    List<Campaign> getDB(){
        return db;
    }
}
