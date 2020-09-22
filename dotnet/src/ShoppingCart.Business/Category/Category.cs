using System;

namespace ShoppingCart.Business.Category
{
    public class Category
    {
        public Guid ID { get; }

        public Guid? ParentCategoryID { get; }

        public string Title { get; }

        public Category(Guid id, Guid? parentCategoryID, string title)
        {
            ID = id;
            ParentCategoryID = parentCategoryID;
            Title = title;
        }
    }
}