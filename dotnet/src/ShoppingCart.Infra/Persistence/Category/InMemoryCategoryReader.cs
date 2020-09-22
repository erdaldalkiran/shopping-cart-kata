using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Business.Category;

namespace ShoppingCart.Infra.Persistence.Category
{
    public class InMemoryCategoryReader : ICategoryReader
    {
        private readonly ICollection<Business.Category.Category> categories;

        public InMemoryCategoryReader(ICollection<Business.Category.Category> categories)
        {
            this.categories = categories;
        }

        public IReadOnlyCollection<Business.Category.Category> GetByIDs(ICollection<Guid> ids)
        {
            return categories.Where(c => ids.Contains(c.ID)).ToList();
        }

        public IReadOnlyCollection<Business.Category.Category> GetAll()
        {
            return categories.ToList();
        }
    }
}