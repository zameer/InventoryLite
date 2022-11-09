﻿using Inventory.Core.Web.Api.Models.Products;

namespace Inventory.Core.Web.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Product> InsertProductAsync(Product product);
        IQueryable<Product> SelectAllProducts();
        ValueTask<Product> SelectProductByIdAsync(Guid productId);
        ValueTask<Product> UpdateProductAsync(Product product);
        ValueTask<Product> DeleteProductAsync(Product product);
    }
}
