using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingCart.Business.Coupon;

namespace ShoppingCart.Business.Cart
{
    public class ApplyCouponCommandHandler : AsyncRequestHandler<ApplyCouponCommand>
    {
        private readonly ICartRepository repository;
        private readonly ICouponReader couponReader;

        public ApplyCouponCommandHandler(
            ICartRepository repository,
            ICouponReader couponReader)
        {
            this.repository = repository;
            this.couponReader = couponReader;
        }

        protected override Task Handle(ApplyCouponCommand request, CancellationToken cancellationToken)
        {
            var cart = EnsureCart(request);

            var coupon = EnsureCoupon(request);

            var isApplicable = coupon.IsApplicable(cart);
            if (!isApplicable) throw new CouponNotApplicableException(coupon.ID, cart.ID);

            cart.ApplyCoupon(coupon);

            return Task.CompletedTask;
        }

        private Cart EnsureCart(ApplyCouponCommand request)
        {
            var cart = repository.GetByID(request.ID);
            if (cart == null) throw new CartNotFoundException(request.ID);

            return cart;
        }

        private Coupon.Coupon EnsureCoupon(ApplyCouponCommand request)
        {
            var coupon = couponReader.GetByIDs(new[] {request.CouponID}).SingleOrDefault();
            if (coupon == null) throw new CouponNotFoundException(request.ID);

            return coupon;
        }
    }
}