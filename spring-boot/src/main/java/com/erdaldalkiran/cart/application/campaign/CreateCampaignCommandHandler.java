package com.erdaldalkiran.cart.application.campaign;

import an.awesome.pipelinr.Command;
import an.awesome.pipelinr.Pipeline;
import an.awesome.pipelinr.Voidy;
import com.erdaldalkiran.cart.application.campaign.CreateCampaignCommand;
import com.erdaldalkiran.cart.application.product.CreateProductCommand;
import com.erdaldalkiran.cart.domain.campaign.Campaign;
import com.erdaldalkiran.cart.domain.campaign.CampaignRepository;
import com.erdaldalkiran.cart.domain.category.CategoryNotFoundException;
import com.erdaldalkiran.cart.domain.category.CategoryReader;
import lombok.SneakyThrows;
import org.springframework.beans.factory.config.ConfigurableBeanFactory;
import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.Collections;
import java.util.concurrent.CompletableFuture;
import java.util.concurrent.ExecutionException;

@Component
@Scope(value = ConfigurableBeanFactory.SCOPE_PROTOTYPE)
public class CreateCampaignCommandHandler implements Command.Handler<CreateCampaignCommand, CompletableFuture<Voidy>> {

    private final CampaignRepository repository;
    private final CategoryReader categoryReader;
    private final Pipeline pipeline;

    public CreateCampaignCommandHandler(
            CampaignRepository repository,
            CategoryReader categoryReader,
            Pipeline pipeline) {
        this.repository = repository;
        this.categoryReader = categoryReader;
        this.pipeline = pipeline;
    }

    @SneakyThrows
    @Override
    public CompletableFuture<Voidy> handle(CreateCampaignCommand command) {
        ensureCategoryExists(command);

        var campaign = new Campaign(command.getId(), command.getCategoryID(), command.getMinimumItemCount(), command.getDiscountType(), command.getRate());
        repository.add(campaign);

        pipeline.send(new CampaignCreatedEvent(command.getId()));

        return CompletableFuture.completedFuture(new Voidy());
    }

    private void ensureCategoryExists(CreateCampaignCommand command) throws InterruptedException, ExecutionException, CategoryNotFoundException {
        var category = categoryReader.getByIDs(Collections.singletonList(command.getCategoryID())).get();
        if (category.isEmpty()) {
            throw new CategoryNotFoundException(command.getCategoryID());
        }
    }
}
