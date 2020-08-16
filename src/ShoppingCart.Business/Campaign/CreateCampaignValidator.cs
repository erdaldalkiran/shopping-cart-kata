using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Validation;

namespace ShoppingCart.Business.Campaign
{
    public class CreateCampaignValidator : IValidator<CreateCampaignCommand>
    {
        public void Validate(CreateCampaignCommand request)
        {
            var errors = new List<string>();
            if (request.ID == default) errors.Add("campaign id cannot be default guid value.");

            if (request.CategoryID == default) errors.Add("campaign category id cannot be default guid value.");

            if (request.MinimumItemCount <= 0)
                errors.Add("campaign minimum item amount cannot be less than or equal to 0.");

            if (request.Rate <= 0m) errors.Add("campaign discount rate cannot be less than or equal to 0.");

            if (!(request.Type == DiscountType.Amount || request.Type == DiscountType.Rate))
                errors.Add("campaign discount type cannot be undefined. use 1 for Rate or 2 for Amount.");

            if (request.Type == DiscountType.Rate && request.Rate > 1)
                errors.Add("discount rate must be between 0 and 1 for rate type discounts.");

            if (errors.Any()) throw new ValidationException(errors);
        }
    }
}