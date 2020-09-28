package com.erdaldalkiran.cart.application.campaign;

import an.awesome.pipelinr.Notification;
import lombok.Getter;

import java.util.UUID;

@Getter
public class CampaignCreatedEvent implements Notification {
    private final UUID id;

    public CampaignCreatedEvent(UUID id) {
        this.id = id;
    }
}
