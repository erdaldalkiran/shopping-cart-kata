using System;
using MediatR;

namespace ShoppingCart.Business.Category
{
    public class CreateCategoryCommand : IRequest
    {
        public Guid ID { get; }

        public Guid? ParentCategoryID { get; }

        public string Title { get; }

        public CreateCategoryCommand(Guid id, Guid? parentCategoryID, string title)
        {
            ID = id;
            ParentCategoryID = parentCategoryID;
            Title = title;
        }
    }
}