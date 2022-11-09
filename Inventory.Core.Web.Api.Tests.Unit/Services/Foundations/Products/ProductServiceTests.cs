// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Inventory.Core.Web.Api.Brokers.Loggings;
using Inventory.Core.Web.Api.Brokers.Storages;
using Inventory.Core.Web.Api.Models.Products;
using Inventory.Core.Web.Api.Services.Foundations.Products;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Inventory.Core.Web.Api.Tests.Unit.Services.Foundations.Students
{
    public partial class ProductServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IProductService productService;

        public ProductServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.productService = new ProductService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Product CreateRandomProduct() =>
            CreateProductFiller(dates: DateTimeOffset.UtcNow).Create();

        private static Product CreateRandomProduct(DateTimeOffset dates) =>
            CreateProductFiller(dates).Create();

        public static TheoryData InvalidMinuteCases()
        {
            int randomMoreThanMinuteFromNow = GetRandomNumber();
            int randomMoreThanMinuteBeforeNow = GetNegativeRandomNumber();

            return new TheoryData<int>
            {
                randomMoreThanMinuteFromNow ,
                randomMoreThanMinuteBeforeNow
            };
        }

        private static Expression<Func<Exception, bool>> SameValidationExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                expectedException.Message == actualException.Message
                && expectedException.InnerException.Message == actualException.InnerException.Message;
        }

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static DateTimeOffset GetRandomDateTime() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() => new IntRange(min: 2, max: 10).GetValue();
        private static int GetNegativeRandomNumber() => -1 * GetRandomNumber();
        private static string GetRandomMessage() => new MnemonicString().GetValue();

        private static IQueryable<Product> CreateRandomProducts(DateTimeOffset dates) =>
            CreateProductFiller(dates).Create(GetRandomNumber()).AsQueryable();

        private static Filler<Product> CreateProductFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Product>();
            Guid productId = Guid.NewGuid();

            filler.Setup()
                .OnProperty(product => product.Id).Use(productId)
                .OnProperty(product => product.Name).IgnoreIt()
                .OnProperty(product => product.Color).IgnoreIt();

            return filler;
        }
    }
}
