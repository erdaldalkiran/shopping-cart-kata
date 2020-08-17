using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Delivery;
using ShoppingCart.Business.Product;

namespace ShoppingCart.Business.Cart
{
    public class AddItemCommandHandler : AsyncRequestHandler<AddItemCommand>
    {
        private readonly ICartRepository repository;
        private readonly IProductReader productReader;
        private readonly DeliveryCostCalculator deliveryCostCalculator;
        private readonly ICampaignFinderService campaignFinderService;

        public AddItemCommandHandler(
            ICartRepository repository,
            IProductReader productReader,
            DeliveryCostCalculator deliveryCostCalculator,
            ICampaignFinderService campaignFinderService)
        {
            this.repository = repository;
            this.productReader = productReader;
            this.deliveryCostCalculator = deliveryCostCalculator;
            this.campaignFinderService = campaignFinderService;
        }

        protected override Task Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var cart = EnsureCart(request);

            AddItem(request, cart);

            ReevaluateCampaign(cart);

            ReevaluateCoupon(cart);

            SetDeliveryCost(cart);

            return Task.CompletedTask;
        }

        private void AddItem(AddItemCommand request, Cart cart)
        {
            var product = productReader.GetByIDs(new[] {request.ProductID}).SingleOrDefault();
            if (product == null) throw new ProductNotFoundException(request.ID);

            cart.AddItem(product, request.Quantity);
        }

        private void SetDeliveryCost(Cart cart)
        {
            var cost = deliveryCostCalculator.CalculateFor(cart);
            cart.SetDeliveryCost(cost);
        }

        private static void ReevaluateCoupon(Cart cart)
        {
            var currentCoupon = cart.AppliedCoupon;
            if (currentCoupon != null) cart.ApplyCoupon(currentCoupon);
        }

        private void ReevaluateCampaign(Cart cart)
        {
            var campaign = campaignFinderService.FindMostApplicableCampaign(cart);
            if (campaign != null)
                cart.ApplyCampaign(campaign);
            else
                cart.ClearCampaign();
        }

        private Cart EnsureCart(AddItemCommand request)
        {
            var cart = repository.GetByID(request.ID);
            if (cart == null) throw new CartNotFoundException(request.ID);

            return cart;
        }
    }
}