using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public class ProductsRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
    }

        public async Task<ProductsDTO> AddProduct(ProductCreateDTO productCreate)
        {
            var product = new Products
            {
                Id = Guid.NewGuid(),
                Name = productCreate.Name,
                DefaultQuantity = productCreate.DefaultQuantity
            };

            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            return new ProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                DefaultQuantity = product.DefaultQuantity
            };
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

        public async Task<UpdateProductsDTO> UpdateProduct(Guid id, UpdateProductsDTO product)
        {
            var updateProduct = await _appDbContext.Products.FindAsync(id);
            if (updateProduct == null)
            {
                return null;
            }
            if (product.Name != null)
            {
                updateProduct.Name = product.Name;
            }
            if(product.DefaultQuantity>0)
            {
                updateProduct.DefaultQuantity = product.DefaultQuantity;
            }
            _appDbContext.Products.Update(updateProduct);
            await _appDbContext.SaveChangesAsync();

            return new UpdateProductsDTO
            {

                Name = updateProduct.Name,
                DefaultQuantity = updateProduct.DefaultQuantity,

            };
        }
        public async Task DeleteProduct(Guid id)
        {
            var product = await _appDbContext.Products.FindAsync(id);
            if (product != null)
            {
                _appDbContext.Products.Remove(product);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
