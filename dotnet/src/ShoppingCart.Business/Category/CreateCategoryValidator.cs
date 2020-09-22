using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.Business.Category
{
    public class CreateCategoryValidator : IValidator<CreateCategoryCommand>
    {
        public void Validate(CreateCategoryCommand request)
        {
            var errors = new List<string>();
            if (request.ID == default) errors.Add("category id cannot be default guid value.");

            if (string.IsNullOrEmpty(request.Title.Trim())) errors.Add("category title cannot be empty.");

            if (errors.Any()) throw new ValidationException(errors);
        }
    }
}