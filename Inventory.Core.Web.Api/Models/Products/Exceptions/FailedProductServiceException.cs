// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Inventory.Core.Web.Api.Models.Students.Exceptions
{
    public class FailedProductServiceException : Xeption
    {
        public FailedProductServiceException(Exception innerException)
            : base(message: "Failed product service error occurred, contact support.", innerException)
        { }
    }
}
