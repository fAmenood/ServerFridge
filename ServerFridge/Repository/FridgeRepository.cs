using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;

namespace ServerFridge.Repository
{
    public class FridgeRepository:IFridgeRepository
    {
        private readonly AppDbContext _appDbContext;

        public FridgeRepository(AppDbContext appDbContext)
        {
            _appDbContext= appDbContext;
        }
        public async Task<List<FridgeDTO>>GetFridges()
        {
            return await _appDbContext.Fridges.Select(f=> new FridgeDTO
            {
                Id = f.Id,
                ModelId = f.ModelId,
                Name= f.Name,
                OwnerName = f.OwnerName,
            })
            .ToListAsync();

        }
        public async Task<List<ProductsDTO>>GetFridgeProducts(Guid id)
        {
            return await _appDbContext.FridgeProducts.Where(fp => fp.FridgeId == id)
                .Include(fp => fp.Products)
                .Select(fp => new ProductsDTO
                {
                    Id=fp.Products.Id,
                    Name = fp.Products.Name,
                    DefaultQuantity=fp.Products.DefaultQuantity,
                  
                    
                }).ToListAsync();
        }

       

        public async Task DeleteFridgeById(Guid id)
        {
            var fridge= await _appDbContext.Fridges.FindAsync(id);
            if (fridge != null)
            {
                _appDbContext.Fridges.Remove(fridge);
                await _appDbContext.SaveChangesAsync();
            }

            
        }

        public async Task<FridgeDTO> GetFridgeById(Guid id)
        {
           var fridge=await _appDbContext.Fridges
                .Include(f=>f.FridgeModel)
                .FirstOrDefaultAsync(f=> f.Id == id);

            if (fridge == null) return null;

            return new FridgeDTO
            {
                Id = fridge.Id,
                Name = fridge.Name,
                ModelId = fridge.ModelId,
                OwnerName = fridge.OwnerName,
            };
        }

        public async Task<FridgeDTO> AddFridge(FridgeCreateDTO fridgeCreate)
        {
            var fridge = new Fridge
            {
                Id = Guid.NewGuid(),
                Name = fridgeCreate.Name,
                OwnerName = fridgeCreate.OwnerName,
                ModelId = fridgeCreate.ModelId
            };

            await _appDbContext.Fridges.AddAsync(fridge);
            await _appDbContext.SaveChangesAsync();

            return new FridgeDTO
            {
                Id = fridge.Id,
                Name = fridge.Name,
                OwnerName = fridge.OwnerName,
                ModelId = fridge.ModelId
            };
        }
        public async Task<UpdateFridgeDTO> UpdateFridge(Guid id,UpdateFridgeDTO fridge)
        {
            var updateFridge = await _appDbContext.Fridges.FindAsync(id);
            if(updateFridge ==null)
            {
                return null;
            }
            if (fridge.Name!=null)
                updateFridge.Name= fridge.Name;


            if(fridge.OwnerName!=null)
                updateFridge.OwnerName= fridge.OwnerName;
            if(fridge.ModelId.HasValue)
            {
                var modelId= await _appDbContext.FridgeModels
                    .AnyAsync(x => x.Id == fridge.ModelId);

                if(modelId==null)
                {
                    throw new ArgumentException("Model doesn't exist");
                }
                updateFridge.ModelId= (Guid)fridge.ModelId;
            }

            _appDbContext.Fridges.Update(updateFridge);
            await _appDbContext.SaveChangesAsync();

            return new UpdateFridgeDTO
            { 
                Name=updateFridge.Name,
                OwnerName=updateFridge.OwnerName,
                ModelId =updateFridge.ModelId,
            };
        }

  




    }
}
