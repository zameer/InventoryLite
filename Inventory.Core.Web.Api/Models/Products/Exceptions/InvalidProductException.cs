// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Xeptions;

namespace Inventory.Core.Web.Api.Models.Products.Exceptions
{
    public class InvalidProductException : Xeption
    {
        public InvalidProductException(string parameterName, object parameterValue)
            : base(message: $"Invalid product, " +
                  $"parameter name: {parameterName}, " +
                  $"parameter value: {parameterValue}.")
        { }

        public InvalidProductException()
            : base(message: "Invalid product. Please fix the errors and try again.") { }
    }
}
