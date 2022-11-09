using Inventory.Core.Web.Api.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Inventory.Core.Web.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Product> Products { get; set; }

        public async ValueTask<Product> InsertProductAsync(Product product)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Product> studentEntityEntry = await broker.Products.AddAsync(entity: product);
            await broker.SaveChangesAsync();

            return studentEntityEntry.Entity;
        }

        public IQueryable<Product> SelectAllProducts() => this.Products;

        public async ValueTask<Product> SelectProductByIdAsync(Guid productId)
        {
            using var broker = new StorageBroker(this.configuration);
            broker.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            return await broker.Products.FindAsync(productId);
        }

        public async ValueTask<Product> UpdateProductAsync(Product product)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Product> productEntityEntry = broker.Products.Update(entity: product);
            await broker.SaveChangesAsync();

            return productEntityEntry.Entity;
        }

        public async ValueTask<Product> DeleteProductAsync(Product product)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Product> productEntityEntry = broker.Products.Remove(entity: product);
            await broker.SaveChangesAsync();

            return productEntityEntry.Entity;
        }
    }
}
