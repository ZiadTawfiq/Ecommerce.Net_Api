using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public interface IProductRepository 
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
