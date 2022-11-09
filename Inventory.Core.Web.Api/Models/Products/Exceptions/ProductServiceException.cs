// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace Inventory.Core.Web.Api.Models.Students.Exceptions
{
    public class ProductServiceException : Exception
    {
        public ProductServiceException(Exception innerException)
            : base(message: "Service error occurred, contact support.", innerException) { }
    }
}
