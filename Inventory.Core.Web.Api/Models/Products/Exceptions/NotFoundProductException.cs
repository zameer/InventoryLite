// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace Inventory.Core.Web.Api.Models.Students.Exceptions
{
    public class NotFoundProductException : Exception
    {
        public NotFoundProductException(Guid studentId)
            : base(message: $"Couldn't find student with id: {studentId}.") { }
    }
}
