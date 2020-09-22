using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ShoppingCart.Business.Cart
{
    public class CreateCartCommandHandler : AsyncRequestHandler<CreateCartCommand>
    {
        private readonly ICartRepository repository;

        public CreateCartCommandHandler(ICartRepository repository)
        {
            this.repository = repository;
        }

        protected override Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Cart(request.ID);
            repository.Add(cart);

            return Task.CompletedTask;
        }
    }
}