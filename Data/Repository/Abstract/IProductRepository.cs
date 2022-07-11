using Core.Repository.Abstract;
using Data.Entity.Collection;

namespace Data.Repository.Abstract;

public interface IProductRepository : IRepository<Product, Guid>
{
    
}