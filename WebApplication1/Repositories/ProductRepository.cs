using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public class ProductRepository(AppDbContext _context) : IProductRepository
    {
        public async Task AddAsync(Product product)
        {
      
            var pro = await _context.Products.FirstOrDefaultAsync(_ => _.Name == product.Name);
            
            if (pro != null) 
            {
                throw new InvalidOperationException("Already, This product exist!");    
            }
            var newpro = new Product
            {
                Name = product.Name,
                StockSize = product.StockSize,
                Price = product.Price
            };
            await _context.Products.AddAsync(newpro);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.StockSize = product.StockSize;
            existingProduct.Price = product.Price;

            await _context.SaveChangesAsync();
        }
    }
}
