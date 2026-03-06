using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public class ModelsRepository : IModelsRepository
    {
        private readonly AppDbContext _appDbContext;

        public ModelsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<FridgeModelDTO>> GetAllModels()
        {
            return await _appDbContext.FridgeModels.Select(f => new FridgeModelDTO
            {
              Id = f.Id,
              Name = f.Name,
              Year = f.Year
            })
            .ToListAsync();

        }
  
        public async Task<FridgeModelsDTO> GetModelById(Guid id)
        {
            var model = await _appDbContext.FridgeModels
                .FirstOrDefaultAsync(f => f.Id == id);

            if (model == null) return null;

            return new FridgeModelsDTO
            {
               
                Name = model.Name,
                Year = model.Year,
                                
            };
        }
    }
}
