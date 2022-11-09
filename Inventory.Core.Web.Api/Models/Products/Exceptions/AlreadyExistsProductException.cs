// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace Inventory.Core.Web.Api.Models.Students.Exceptions
{
    public class AlreadyExistsProductException : Xeption
    {
        public AlreadyExistsProductException(Exception innerException)
            : base(message: "Product with the same id already exists.", innerException) { }
    }
}
