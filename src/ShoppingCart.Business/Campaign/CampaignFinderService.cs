using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Business.Campaign
{
    public interface ICampaignFinderService
    {
        Campaign FindMostApplicableCampaignTo(Cart.Cart cart);
    }

    public class CampaignFinderService : ICampaignFinderService
    {
        private readonly ICampaignReader campaignReader;

        public CampaignFinderService(ICampaignReader campaignReader)
        {
            this.campaignReader = campaignReader;
        }

        public Campaign FindMostApplicableCampaignTo(Cart.Cart cart)
        {
            var applicableCampaigns = GetApplicableCampaigns(cart);

            var results = SimulateCampaigns(applicableCampaigns, cart);

            var campaigns = SortCampaignsByDiscountAmount(results);

            return campaigns.FirstOrDefault();
        }

        private static IList<Campaign> SortCampaignsByDiscountAmount(ConcurrentBag<KeyValuePair<Campaign, decimal?>> results)
        {
            return results
                .Where(kv => kv.Value.HasValue)
                .OrderByDescending(kv => kv.Value.Value)
                .Select(kv => kv.Key)
                .ToList();
        }

        private static ConcurrentBag<KeyValuePair<Campaign, decimal?>> SimulateCampaigns(
            IList<Campaign> applicableCampaigns, Cart.Cart cart)
        {
            var results = new ConcurrentBag<KeyValuePair<Campaign, decimal?>>();
            Parallel.ForEach(applicableCampaigns, campaign =>
            {
                var discountAmount = campaign.CalculateDiscountAmountFor(cart);
                results.Add(new KeyValuePair<Campaign, decimal?>(campaign, discountAmount));
            });
            return results;
        }

        private IList<Campaign> GetApplicableCampaigns(Cart.Cart cart)
        {
            var categoryIDs = cart.LineItems.Select(l => l.Product.CategoryID).Distinct().ToList();
            return campaignReader.GetByCategories(categoryIDs).ToList();
        }
    }
}