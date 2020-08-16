using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.Business.Product
{
    public class CreateProductValidator : IValidator<CreateProductCommand>
    {
        public void Validate(CreateProductCommand request)
        {
            var errors = new List<string>();
            if (request.ID == default) errors.Add("product id cannot be default guid value.");

            if (string.IsNullOrEmpty(request.Title.Trim())) errors.Add("product title cannot be empty.");

            if (request.CategoryID == default) errors.Add("product category id cannot be default guid value.");

            if (request.Price <= 0m) errors.Add("product price cannot be less than or equal to 0.");

            if (errors.Any()) throw new ValidationException(errors);
        }
    }
}