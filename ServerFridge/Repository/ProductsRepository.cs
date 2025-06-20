using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;

namespace ServerFridge.Repository
{
    public class ProductsRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
    }

        public async Task<ProductsDTO> AddProduct(ProductsDTO product)
        {
           var newProduct
        }

        public async Task<ProductsDTO> GetProductById(Guid id)
        {
            var product = await _appDbContext.Products
                 .FirstOrDefaultAsync(f => f.Id == id);

            if (product == null) return null;

            return new ProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                DefaultQuantity = product.DefaultQuantity,
               
            };
        }

        public async Task<List<ProductsDTO>> GetProducts()
        {
            return await _appDbContext.Products.Select(x => new ProductsDTO
            {
                Id=x.Id,
                Name=x.Name,
                DefaultQuantity=x.DefaultQuantity,


            }).ToListAsync();
        }

        Task<ProductsDTO> IProductRepository.UpdateProduct(Guid id, UpdateFridgeDTO fridge)
        {
            throw new NotImplementedException();
        }
    }
}
