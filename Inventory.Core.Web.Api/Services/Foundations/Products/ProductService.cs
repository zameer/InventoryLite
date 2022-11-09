// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Core.Web.Api.Brokers.Loggings;
using Inventory.Core.Web.Api.Brokers.Storages;
using Inventory.Core.Web.Api.Models.Products;

namespace Inventory.Core.Web.Api.Services.Foundations.Products
{
    public partial class ProductService : IProductService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public ProductService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Product> AddProductAsync(Product product) =>
        TryCatch(async () =>
        {
            ValidateProductOnCreate(product);

            throw new NotImplementedException();//await this.storageBroker.InsertProductAsync(product);
        });

        public IQueryable<Product> RetrieveAllProducts() =>
        TryCatch(() => this.storageBroker.SelectAllProducts());

        public ValueTask<Product> RetrieveProductByIdAsync(Guid productId) =>
        TryCatch(async () =>
        {
            ValidateProductId(productId);
            Product maybeProduct = await this.storageBroker.SelectProductByIdAsync(productId);
            ValidateStorageProduct(maybeProduct, productId);

            return maybeProduct;
        });

        public ValueTask<Product> ModifyProductAsync(Product product) =>
        TryCatch(async () =>
        {
            ValidateProductOnModify(product);

            Product maybeProduct =
                await this.storageBroker.SelectProductByIdAsync(product.Id);

            ValidateStorageProduct(maybeProduct, product.Id);

            return await this.storageBroker.UpdateProductAsync(product);
        });

        public ValueTask<Product> RemoveProductByIdAsync(Guid productId) =>
            TryCatch(async () =>
         {
             ValidateProductId(productId);

             Product maybeProduct =
                 await this.storageBroker.SelectProductByIdAsync(productId);

             ValidateStorageProduct(maybeProduct, productId);

             return await this.storageBroker.DeleteProductAsync(maybeProduct);
         });
    }
}
