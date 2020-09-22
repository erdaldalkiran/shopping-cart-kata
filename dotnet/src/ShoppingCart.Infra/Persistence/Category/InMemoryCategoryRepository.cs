using System.Collections.Generic;
using ShoppingCart.Business.Category;

namespace ShoppingCart.Infra.Persistence.Category
{
    public class InMemoryCategoryRepository : ICategoryRepository
    {
        private readonly ICollection<Business.Category.Category> categories;

        public InMemoryCategoryRepository(ICollection<Business.Category.Category> categories)
        {
            this.categories = categories;
        }

        public void Add(Business.Category.Category category)
        {
            categories.Add(category);
        }
    }
}