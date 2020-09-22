using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.Business.Cart
{
    public class ApplyCouponValidator : IValidator<ApplyCouponCommand>
    {
        public void Validate(ApplyCouponCommand request)
        {
            var errors = new List<string>();

            if (request.ID == default) errors.Add("cart id cannot be default guid value.");

            if (request.CouponID == default) errors.Add("coupon id cannot be default guid value.");

            if (errors.Any()) throw new ValidationException(errors);
        }
    }
}