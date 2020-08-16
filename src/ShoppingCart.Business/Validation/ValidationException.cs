using System;
using System.Collections.Generic;

namespace ShoppingCart.Business.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(IList<string> errors)
            : base(string.Join(Environment.NewLine, errors))
        {
        }
    }
}