using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Campaign;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.Business.Coupon
{
    public class CreateCouponValidator : IValidator<CreateCouponCommand>
    {
        public void Validate(CreateCouponCommand request)
        {
            var errors = new List<string>();
            if (request.ID == default) errors.Add("coupon id cannot be default guid value.");

            if (request.MinimumCartAmount <= 0m)
                errors.Add("coupon minimum cart amount cannot be less than or equal to 0.");

            if (request.Rate <= 0m) errors.Add("coupon discount rate cannot be less than or equal to 0.");

            if (!(request.Type == DiscountType.Amount || request.Type == DiscountType.Rate))
                errors.Add("coupon discount type cannot be undefined. use 1 for Rate or 2 for Amount.");

            if (request.Type == DiscountType.Rate && request.Rate > 1)
                errors.Add("discount rate must be between 0 and 1 for rate type discounts.");

            if (errors.Any()) throw new ValidationException(errors);
        }
    }
}