using System;

namespace ShoppingCart.Business.Domain
{
    public class Category
    {
        public Guid Id { get; }

        public Guid? ParentCategoryID { get; }

        public string Title { get; }

        public Category(Guid id, Guid parentCategoryID, string title)
        {
            Id = id;
            ParentCategoryID = parentCategoryID;
            Title = title;
        }
    }
}