using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public interface IFridgeRepository
    {
        Task<List<Fridge>> GetFridges();

        Task<Fridge> GetFridgeById(Guid id);

        Task<Fridge> AddFridge(Fridge fridge);

        Task<Fridge> UpdateFridge(Fridge fridge);

        void DeleteFridgeById(Guid id);

        Task<List<FridgeProducts>> GetFridgeProducts(Guid id);



    }
}
