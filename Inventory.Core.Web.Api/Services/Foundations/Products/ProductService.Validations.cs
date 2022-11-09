// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Data;
using Inventory.Core.Web.Api.Models.Products;
using Inventory.Core.Web.Api.Models.Products.Exceptions;
using Inventory.Core.Web.Api.Models.Students.Exceptions;

namespace Inventory.Core.Web.Api.Services.Foundations.Products
{
    public partial class ProductService
    {
        private void ValidateProductOnCreate(Product product)
        {
            ValidateProduct(product);

            Validate(
                (Rule: IsInvalidX(product.Id), Parameter: nameof(Product.Id)),
                (Rule: IsInvalidX(product.Name), Parameter: nameof(Product.Name)),
                (Rule: IsInvalidX(product.Color), Parameter: nameof(Product.Color)),
                (Rule: IsInvalidX(product.SKU), Parameter: nameof(Product.SKU))
            );
        }

        private void ValidateProductOnModify(Product product)
        {
            ValidateProduct(product);
            Validate(
                (Rule: IsInvalidX(product.Id), Parameter: nameof(Product.Id)),
                (Rule: IsInvalidX(product.Name), Parameter: nameof(Product.Name)),
                (Rule: IsInvalidX(product.Color), Parameter: nameof(Product.Color)),
                (Rule: IsInvalidX(product.SKU), Parameter: nameof(Product.SKU))
            );
        }

        private static dynamic IsInvalidX(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalidX(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalidX(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsNotSame(
            Guid firstId,
            Guid secondId,
            string secondIdName) => new
            {
                Condition = firstId != secondId,
                Message = $"Id is not the same as {secondIdName}"
            };

        private static dynamic IsNotSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate != secondDate,
                Message = $"Date is not the same as {secondDateName}"
            };

        private static dynamic IsSame(
            DateTimeOffset firstDate,
            DateTimeOffset secondDate,
            string secondDateName) => new
            {
                Condition = firstDate == secondDate,
                Message = $"Date is the same as {secondDateName}"
            };

        private static void ValidateProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new InvalidProductException(
                    parameterName: nameof(Product.Id),
                    parameterValue: productId);
            }
        }

        private static void ValidateStorageProduct(Product storageProduct, Guid productId)
        {
            if (storageProduct is null)
            {
                throw new NotFoundProductException(productId);
            }
        }

        private static void ValidateProduct(Product product)
        {
            if (product is null)
            {
                throw new NullProductException();
            }
        }

        private static bool IsInvalid(string input) => String.IsNullOrWhiteSpace(input);
        private static bool IsInvalid(Guid input) => input == default;

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidProductException = new InvalidProductException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidProductException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidProductException.ThrowIfContainsErrors();
        }
    }
}