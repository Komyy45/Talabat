using System.Collections.Concurrent;
using Linkdev.Talabat.Core.Domain.Contracts;
using Linkdev.Talabat.Persistence.Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Linkdev.Talabat.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;

        private readonly Lazy<IGenericRepository<Product, int>> _productRepository;
        private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandRepository;
        private readonly Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;

        public IGenericRepository<Product, int> ProductRepository => _productRepository.Value;
        public IGenericRepository<ProductBrand, int> BrandRepository => _brandRepository.Value;
        public IGenericRepository<ProductCategory, int> CategoryRepository => _categoryRepository.Value;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;

            _productRepository = new (() => new GenericRepository<Product,int>(_dbContext));
            _brandRepository = new (() => new GenericRepository<ProductBrand,int>(_dbContext));
            _categoryRepository = new (() => new GenericRepository<ProductCategory,int>(_dbContext));
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    }
}
