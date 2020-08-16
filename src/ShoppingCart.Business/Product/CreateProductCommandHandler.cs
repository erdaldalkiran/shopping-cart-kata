using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingCart.Business.Category;

namespace ShoppingCart.Business.Product
{
    public class CreateProductCommandHandler : AsyncRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository repository;
        private readonly ICategoryReader categoryReader;

        public CreateProductCommandHandler(
            IProductRepository repository,
            ICategoryReader categoryReader)
        {
            this.repository = repository;
            this.categoryReader = categoryReader;
        }

        protected override Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = categoryReader.GetByIDs(new List<Guid> {request.CategoryID}).FirstOrDefault();
            if (category == null) throw new CategoryNotFoundException(request.CategoryID);

            var product = new Product(request.ID, request.Title, request.Price, request.CategoryID);
            repository.Add(product);

            return Task.CompletedTask;
        }
    }
}