using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingCart.Business.Campaign;

namespace ShoppingCart.Business.Cart
{
    //this is not the best way to handle events because in the same transaction we are dealing with multiple aggregates
    //it's here to simplify usage. in real world, we should use a message bus to handle events.
    public class CampaignCreatedEventHandler : INotificationHandler<CampaignCreatedEvent>
    {
        private readonly ICartRepository repository;
        private readonly ICampaignFinderService campaignFinderService;

        public CampaignCreatedEventHandler(ICartRepository repository, ICampaignFinderService campaignFinderService)
        {
            this.repository = repository;
            this.campaignFinderService = campaignFinderService;
        }

        public Task Handle(CampaignCreatedEvent notification, CancellationToken cancellationToken)
        {
            var carts = repository.GetAll();

            Parallel.ForEach(carts, cart =>
            {
                ReevaluateCampaign(cart);
                ReevaluateCoupon(cart);
            });

            return Task.CompletedTask;
        }

        private static void ReevaluateCoupon(Cart cart)
        {
            var currentCoupon = cart.AppliedCoupon;
            if (currentCoupon != null) cart.ApplyCoupon(currentCoupon);
        }

        private void ReevaluateCampaign(Cart cart)
        {
            var campaign = campaignFinderService.FindMostApplicableCampaignTo(cart);
            if (campaign != null)
                cart.ApplyCampaign(campaign);
            else
                cart.ClearCampaign();
        }
    }
}