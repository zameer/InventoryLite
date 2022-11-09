// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Inventory.Core.Web.Api.Models.Products;
using Inventory.Core.Web.Api.Models.Products.Exceptions;
using Inventory.Core.Web.Api.Models.Students.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace Inventory.Core.Web.Api.Services.Foundations.Products
{
    public partial class ProductService
    {
        private delegate ValueTask<Product> ReturningProductFunction();
        private delegate IQueryable<Product> ReturningProductsFunction();

        private async ValueTask<Product> TryCatch(ReturningProductFunction returningProductFunction)
        {
            try
            {
                return await returningProductFunction();
            }
            catch (NullProductException nullProductException)
            {
                throw CreateAndLogValidationException(nullProductException);
            }
            catch (InvalidProductException invalidProductException)
            {
                throw CreateAndLogValidationException(invalidProductException);
            }
            catch (NotFoundProductException notFoundProductException)
            {
                throw CreateAndLogValidationException(notFoundProductException);
            }
            catch (Exception exception)
            {
                var failedProductServiceException =
                    new FailedProductServiceException(exception);

                throw CreateAndLogServiceException(failedProductServiceException);
            }
        }

        private IQueryable<Product> TryCatch(ReturningProductsFunction returningProductsFunction)
        {
            try
            {
                return returningProductsFunction();
            }
            catch (SqlException sqlException)
            {
                throw CreateAndLogCriticalDependencyException(sqlException);
            }
            catch (Exception exception)
            {
                var failedProductServiceException =
                    new FailedProductServiceException(exception);

                throw CreateAndLogServiceException(failedProductServiceException);
            }
        }

        private ProductServiceException CreateAndLogServiceException(Exception exception)
        {
            var productServiceException = new ProductServiceException(exception);
            this.loggingBroker.LogError(productServiceException);

            return productServiceException;
        }

        private ProductDependencyException CreateAndLogCriticalDependencyException(Exception exception)
        {
            var productDependencyException = new ProductDependencyException(exception);
           
            this.loggingBroker.LogCritical(productDependencyException);

            return productDependencyException;
        }

        private ProductValidationException CreateAndLogValidationException(Exception exception)
        {
            var productValidationException = new ProductValidationException(exception);
            this.loggingBroker.LogError(productValidationException);

            return productValidationException;
        }
    }
}
