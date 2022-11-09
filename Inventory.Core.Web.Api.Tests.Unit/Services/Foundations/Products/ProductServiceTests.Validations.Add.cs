// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Inventory.Core.Web.Api.Models.Products;
using Inventory.Core.Web.Api.Models.Products.Exceptions;
using Inventory.Core.Web.Api.Models.Students.Exceptions;
using Moq;
using Xunit;

namespace Inventory.Core.Web.Api.Tests.Unit.Services.Foundations.Students
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async void ShouldThrowValidationExceptionOnRetrieveWhenIdIsInvalidAndLogItAsync()
        {
            // given
            Guid randomProductId = default;
            Guid inputProductId = randomProductId;

            var invalidProductException = new InvalidProductException(
                parameterName: nameof(Product.Id),
                parameterValue: inputProductId);

            var expectedProductValidationException =
                new ProductValidationException(invalidProductException);

            // when
            ValueTask<Product> retrieveStudentByIdTask =
                this.productService.RetrieveProductByIdAsync(inputProductId);

            // then
            await Assert.ThrowsAsync<ProductValidationException>(() =>
                retrieveStudentByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedProductValidationException))),
                    Times.Once);


            this.storageBrokerMock.Verify(broker =>
                broker.SelectProductByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
