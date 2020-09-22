using System;

namespace ShoppingCart.API.Category
{
    public class CreateCategoryRequest
    {
        public Guid? ParentCategoryID { get; set; }

        public string Title { get; set; }
    }
}