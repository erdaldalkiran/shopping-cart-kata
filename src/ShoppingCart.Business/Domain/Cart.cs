using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShoppingCart.Business.Domain
{
    public class Cart
    {
        public ICollection<LineItem> LineItems { get; }

        public Cart()
        {
            LineItems = new Collection<LineItem>();
        }

        public IReadOnlyCollection<LineItem> GetLineItems()
        {
            return (IReadOnlyCollection<LineItem>) LineItems;
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null) throw new ArgumentNullException($"{nameof(product)} cannot be null.");

            if (quantity < 1) throw new ArgumentException($"{nameof(quantity)} must be greater than 0.");

            var inCartProductMaybe = LineItems.FirstOrDefault(l => l.Product.Id == product.Id);
            if (inCartProductMaybe == null)
            {
                LineItems.Add(new LineItem(product, quantity));
                return;
            }

            //ASSUMPTION: last added product has the most current information
            LineItems.Remove(inCartProductMaybe);

            var inCartProductQuantity = inCartProductMaybe.Quantity;
            LineItems.Add(new LineItem(product, quantity + inCartProductQuantity));
        }


        public Price TotalAmount()
        {
            return LineItems
                .Select(l => l.Product.Price * l.Quantity)
                .Aggregate((acc, p) => acc + p);
        }
    }
}