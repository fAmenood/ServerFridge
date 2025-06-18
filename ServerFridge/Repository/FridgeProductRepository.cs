using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;

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
        public async Task<IActionResult> AddProductToFridge(FridgeProductsDTO fridgeProductsDTO)
        {

        }
    }
}
