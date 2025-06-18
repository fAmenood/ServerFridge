using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public class FridgeProductRepository : IFridgeProductRepository
    {
        public readonly AppDbContext _appDbContext;
        public FridgeProductRepository(AppDbContext appDbContext)
        {
            _appDbContext=appDbContext;
        }
        public async Task<List<FridgeProductsDTO>> GetFridgeProducts()
        {
            return await _appDbContext.FridgeProducts.Select(x => new FridgeProductsDTO
            {
                ProductId = x.ProductId,
                FridgeId = x.FridgeId,
                Id = x.Id,
                Quantity = x.Quantity,

                
            }).ToListAsync();
             
        }
        public async Task<FridgeProductsDTO> GetFridgeProductsById(Guid id)
        {
            var fridgeProd= await _appDbContext.FridgeProducts
              
                .FirstOrDefaultAsync(fp => fp.Id == id);

            if (fridgeProd == null) return null;

            return new FridgeProductsDTO
            {
                Id = fridgeProd.Id,
                FridgeId = fridgeProd.FridgeId,
                ProductId = fridgeProd.ProductId,
                Quantity = fridgeProd.Quantity
            };
                



            
        }
        public async Task<FridgeProductsDTO> AddProductToFridge(FridgeProductsDTO fridgeProductsDTO)
        {
            var fridge = await _appDbContext.Fridges
                .AnyAsync(f => f.Id == fridgeProductsDTO.FridgeId);

            if(!fridge)
            {
                throw new ArgumentException($"Fridge with id {fridgeProductsDTO.FridgeId} doesn't exist");

            }
            var products = await _appDbContext.Products
                .AnyAsync(pr=>pr.Id== fridgeProductsDTO.ProductId); ;
            if(!products)
            {
                throw new ArgumentException($"Product with id {fridgeProductsDTO.ProductId} doesn't exist");
            }
            var relation = await _appDbContext.FridgeProducts
                .FirstOrDefaultAsync
                (fp => fp.FridgeId == fridgeProductsDTO.FridgeId &&
                 fp.ProductId == fridgeProductsDTO.ProductId);
            if (relation != null)
            {
                relation.Quantity += fridgeProductsDTO.Quantity;
            }
            else
            {
                var fridProds = new FridgeProducts
                {
                    FridgeId = fridgeProductsDTO.FridgeId,
                    ProductId = fridgeProductsDTO.ProductId,
                    Quantity = fridgeProductsDTO.Quantity,


                };
                await _appDbContext.FridgeProducts.AddAsync(fridProds);
            }
            
            await _appDbContext.SaveChangesAsync(); 

            

            return fridgeProductsDTO;
        }
        public async Task<FridgeProductsDTO> UpdateFridgeProducts(Guid id, FridgeProductsDTO fridgeProductsDTO)
        {
            var frProds = await _appDbContext.FridgeProducts.FindAsync(id);
            if (frProds==null)
            {
                return null;
            }
            if(fridgeProductsDTO.Quantity>=0)
                frProds.Quantity = fridgeProductsDTO.Quantity;

            _appDbContext.FridgeProducts.Update(frProds);

            await _appDbContext.SaveChangesAsync();

            return new FridgeProductsDTO
            {
                Id=frProds.Id,
                FridgeId=frProds.FridgeId,
                ProductId=frProds.ProductId,
                Quantity=frProds.Quantity,
                
            };
        }
        public async Task<bool> DeleteFridgeProducts(Guid id)
        {
            var delFrProds = await _appDbContext.FridgeProducts.FindAsync(id);
            if(delFrProds == null) return false;

            _appDbContext.FridgeProducts.Remove(delFrProds);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<int> ZeroQuantityFridgeProducts()
        {
            return await _appDbContext.Database.ExecuteSqlRawAsync("EXEC ZeroQuantityFridgeProductes");
        }

    }
}
