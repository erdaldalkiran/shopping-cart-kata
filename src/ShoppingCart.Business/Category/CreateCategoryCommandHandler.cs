using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ShoppingCart.Business.Category
{
    public class CreateCategoryCommandHandler : AsyncRequestHandler<CreateCategoryCommand>
    {
        private readonly ICategoryRepository repository;
        private readonly ICategoryReader categoryReader;

        public CreateCategoryCommandHandler(ICategoryRepository repository, ICategoryReader categoryReader)
        {
            this.repository = repository;
            this.categoryReader = categoryReader;
        }

        protected override Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentCategoryID.HasValue)
            {
                var parent = categoryReader.GetByIDs(new List<Guid> {request.ParentCategoryID.Value}).SingleOrDefault();
                if (parent == null) throw new CategoryNotFoundException(request.ParentCategoryID.Value);
            }

            var category = new Category(request.ID, request.ParentCategoryID, request.Title);
            repository.Add(category);

            return Task.CompletedTask;
        }
    }
}