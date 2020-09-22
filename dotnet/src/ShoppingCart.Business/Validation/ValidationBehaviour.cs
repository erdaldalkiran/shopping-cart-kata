using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ShoppingCart.Business.Validation
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> validator;

        public ValidationBehaviour(IValidator<TRequest> validator = null)
        {
            this.validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            validator?.Validate(request);

            return await next();
        }
    }
}