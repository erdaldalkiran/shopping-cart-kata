using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ShoppingCart.Business.Domain;
using ShoppingCart.Business.Readers;
using ShoppingCart.Business.Services;

namespace ShoppingCart.UnitTests.Services.CampaignFinderServiceTests.When_finding_most_applicable_campaign
{
    [Description(
        "There are 3 campaigns their categories does not match any line item in cart. We expect no applicable campaign to be find.")]
    internal class And_there_is_no_applicable_campaign
    {
        private readonly Mock<ICampaignReader> campaignReaderService = new Mock<ICampaignReader>();

        private readonly IList<Campaign> campaigns = new List<Campaign>(3);
        private readonly Cart cart = new Cart(Guid.NewGuid());


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SetupData();
        }

        [Test]
        public void no_campaign_should_be_returned()
        {
            var service = new CampaignFinderService(campaignReaderService.Object);
            var campaign = service.FindMostApplicableCampaign(cart);

            campaign.Should().BeNull();
        }

        private void SetupData()
        {
            var categoryID = Guid.NewGuid();

            var products = new List<Product>(3)
            {
                new Product(Guid.NewGuid(), "ProductA", 100m, categoryID),
                new Product(Guid.NewGuid(), "ProductB", 200m, categoryID),
                new Product(Guid.NewGuid(), "ProductC", 300m, categoryID)
            };

            products.ForEach(p => cart.AddItem(p, 1));

            Enumerable.Range(1, 3)
                .Select(i => new Campaign(Guid.NewGuid(), Guid.NewGuid(), 1, DiscountType.Rate, 0.10m))
                .ToList()
                .ForEach(campaigns.Add);

            campaignReaderService
                .Setup(s => s.GetByCategories(It.IsAny<ICollection<Guid>>()))
                .Returns(campaigns);
        }
    }
}