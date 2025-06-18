using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public class FridgeRepository:IFridgeRepository
    {
        private readonly AppDbContext _appDbContext;

        public FridgeRepository(AppDbContext appDbContext)
        {
            _appDbContext= appDbContext;
        }
        public async Task<List<Fridge>>GetFridges()
        {
            return await _appDbContext.Fridges.ToListAsync();

        }
        public async Task<List<FridgeProducts>>GetFridgeProducts(Guid id)
        {
            return await _appDbContext.FridgeProducts.Include(fp => fp.Products).Where(fp=>fp.FridgeId==id).ToListAsync();
        }

        Task<Fridge> IFridgeRepository.AddFridgeAsync(Fridge fridge)
        {
            throw new NotImplementedException();
        }

        void IFridgeRepository.DeleteFridgeById(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Fridge> IFridgeRepository.GetFridgeById(Guid id)
        {
            throw new NotImplementedException();
        }

   
        Task<Fridge> IFridgeRepository.UpdateFridgeAsync(Fridge fridge)
        {
            throw new NotImplementedException();
        }
    }
}
