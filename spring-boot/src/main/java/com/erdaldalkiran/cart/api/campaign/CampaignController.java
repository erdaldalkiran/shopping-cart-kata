package com.erdaldalkiran.cart.api.campaign;

import an.awesome.pipelinr.Pipeline;
import com.erdaldalkiran.cart.api.campaign.CreateCampaignRequest;
import com.erdaldalkiran.cart.application.campaign.CreateCampaignCommand;
import com.erdaldalkiran.cart.domain.campaign.Campaign;
import com.erdaldalkiran.cart.domain.campaign.CampaignReader;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.validation.Valid;
import java.net.URI;
import java.util.Collection;
import java.util.Collections;
import java.util.UUID;
import java.util.concurrent.ExecutionException;

@RestController
@RequestMapping("/campaign")
public class CampaignController {
    private final Pipeline pipeline;
    private final CampaignReader reader;

    public CampaignController(Pipeline pipeline, CampaignReader reader) {
        this.pipeline = pipeline;
        this.reader = reader;
    }

    @GetMapping("/id/{id}")
    public ResponseEntity<Campaign> getByID(@PathVariable UUID id) throws ExecutionException, InterruptedException {
        var campaign = reader.getByIDs(Collections.singletonList(id)).get();
        if (campaign.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(campaign.get(0));
    }

    @GetMapping("/all")
    public ResponseEntity<Collection<Campaign>> getAll() throws ExecutionException, InterruptedException {
        var campaigns = reader.getAll().get();
        if (campaigns.isEmpty()) {
            return ResponseEntity.notFound().build();
        }

        return ResponseEntity.ok(campaigns);
    }

    @PostMapping()
    public ResponseEntity<Void> create(@Valid @RequestBody CreateCampaignRequest request) {
        final var id = UUID.randomUUID();
        var command = new CreateCampaignCommand(id, request.getCategoryID(), request.getMinimumItemCount(), request.getDiscountType(), request.getRate());
        command.execute(pipeline);

        return ResponseEntity.created(URI.create("/campaign/id/" + id)).build();
    }
}
