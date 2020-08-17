using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.Business.Cart
{
    public class AddItemValidator : IValidator<AddItemCommand>
    {
        public void Validate(AddItemCommand request)
        {
            var errors = new List<string>();

            if (request.ID == default) errors.Add("cart id cannot be default guid value.");

            if (request.ProductID == default) errors.Add("product id cannot be default guid value.");

            if (request.Quantity <= 0) errors.Add("quantity cannot be less than or equal to 0.");

            if (errors.Any()) throw new ValidationException(errors);
        }
    }
}