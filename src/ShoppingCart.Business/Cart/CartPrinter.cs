using System.Linq;
using System.Text;
using ShoppingCart.Business.Category;

namespace ShoppingCart.Business.Cart
{
    public class CartPrinter
    {
        private readonly ICategoryReader categoryReader;

        private static readonly string LineItemHeader =
            "CategoryName ProductName Quantity UnitPrice TotalPrice TotalDiscount";

        private static readonly string LineItemFormat = "{0} {1} {2} {3}TL {4}TL {5}TL";
        private static readonly string CartFormat = "Total Amount:{0}TL Delivery Cost: {1}TL";

        public CartPrinter(ICategoryReader categoryReader)
        {
            this.categoryReader = categoryReader;
        }

        public string Print(Cart cart)
        {
            var lineItems = cart.GetLineItems().OrderBy(l => l.Product.CategoryID).ToList();
            var distinctCategories = lineItems.Select(l => l.Product.CategoryID).Distinct().ToList();
            var categories = categoryReader.GetByIDs(distinctCategories)
                .ToDictionary(c => c.ID, c => c.Title);

            var sb = new StringBuilder();
            sb.AppendLine(LineItemHeader);
            lineItems.ForEach(l =>
            {
                var line = string.Format(
                    LineItemFormat,
                    categories[l.Product.CategoryID],
                    l.Product.Title,
                    l.Quantity,
                    l.Product.Price,
                    l.TotalAmountAfterDiscounts,
                    l.TotalDiscount);

                sb.AppendLine(line);
            });

            sb.AppendLine(string.Format(CartFormat, cart.TotalAmountAfterDiscounts, cart.GetDeliveryCost()));

            return sb.ToString();
        }
    }
}