using Data.Entity.Collection;
using Data.Repository.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Data.Repository;

public class ProductRepository : MongoDbRepositoryBase<Product>, IProductRepository
{
    public ProductRepository(IOptions<MongoDbSettings> options, ILogger<ProductRepository> logger) : base(options, logger)
    {
    }
}