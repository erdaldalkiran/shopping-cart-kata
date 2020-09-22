using System;

namespace ShoppingCart.Business.Category
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException(Guid id)
            : base($"category {id} not found")
        {
        }
    }
}