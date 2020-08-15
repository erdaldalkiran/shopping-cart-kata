using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Business.Domain;
using ShoppingCart.Business.Readers;

namespace ShoppingCart.Business.Services
{
    public interface ICampaignFinderService
    {
        Campaign FindMostApplicableCampaign(Cart cart);
    }

    public class CampaignFinderService : ICampaignFinderService
    {
        private readonly ICampaignReader campaignReader;

        public CampaignFinderService(ICampaignReader campaignReader)
        {
            this.campaignReader = campaignReader;
        }

        public Campaign FindMostApplicableCampaign(Cart cart)
        {
            var applicableCampaigns = GetApplicableCampaigns(cart);

            var results = SimulateCampaigns(applicableCampaigns, cart);

            var campaigns = GetCampaignsApplied(results);

            return campaigns.FirstOrDefault();
        }

        private static IList<Campaign> GetCampaignsApplied(ConcurrentBag<KeyValuePair<Campaign, decimal?>> results)
        {
            return results
                .Where(kv => kv.Value.HasValue)
                .OrderByDescending(kv => kv.Value.Value)
                .Select(kv => kv.Key)
                .ToList();
        }

        private static ConcurrentBag<KeyValuePair<Campaign, decimal?>> SimulateCampaigns(
            IList<Campaign> applicableCampaigns, Cart cart)
        {
            var results = new ConcurrentBag<KeyValuePair<Campaign, decimal?>>();
            Parallel.ForEach(applicableCampaigns, campaign =>
            {
                var discountAmount = campaign.CalculateDiscountAmount(cart);
                results.Add(new KeyValuePair<Campaign, decimal?>(campaign, discountAmount));
            });
            return results;
        }

        private IList<Campaign> GetApplicableCampaigns(Cart cart)
        {
            var categoryIDs = cart.GetLineItems().Select(l => l.Product.CategoryID).Distinct().ToList();
            return campaignReader.GetByCategories(categoryIDs).ToList();
        }
    }
}