using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class ProductService(IProductRepository repository, IMapper _mapper) : IProductService
    {
        public async Task AddAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await repository.AddAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(id); 
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await repository.GetAllAsync()); 
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return _mapper.Map<ProductDto>(await repository.GetByIdAsync(id));
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            await repository.UpdateAsync(_mapper.Map<Product>(productDto)); 
        }
    }
}
