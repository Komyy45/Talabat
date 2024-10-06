using Linkdev.Talabat.Core.Domain.Entities.Products;

namespace Linkdev.Talabat.Core.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<Product, int> ProductRepository { get; }
        IGenericRepository<ProductBrand, int> BrandRepository { get; }
        IGenericRepository<ProductCategory, int> CategoryRepository { get; }
        Task<int> CompleteAsync();
    }
}
