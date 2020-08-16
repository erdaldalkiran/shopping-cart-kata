using ShoppingCart.Business.Campaign;

namespace ShoppingCart.API.Coupon
{
    public class CreateCouponRequest
    {
        public decimal MinimumCartAmount { get; set; }

        public DiscountType Type { get; set; }

        public decimal Rate { get; set; }
    }
}