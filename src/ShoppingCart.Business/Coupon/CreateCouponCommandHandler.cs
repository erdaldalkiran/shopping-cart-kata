using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ShoppingCart.Business.Coupon
{
    public class CreateCouponCommandHandler : AsyncRequestHandler<CreateCouponCommand>
    {
        private readonly ICouponRepository repository;

        public CreateCouponCommandHandler(ICouponRepository repository)
        {
            this.repository = repository;
        }

        protected override Task Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = new Coupon(request.ID, request.MinimumCartAmount, request.Type, request.Rate);
            repository.Add(coupon);

            return Task.CompletedTask;
        }
    }
}