using ServerFridge.Models;
using ServerFridge.DTOModels;
namespace ServerFridge.Repository
{
    public interface IFridgeRepository
    {
        Task<List<FridgeDTO>> GetFridges();

        Task<FridgeDTO> GetFridgeById(Guid id);

        Task<FridgeDTO> AddFridge(FridgeDTO fridge);

        Task<UpdateFridgeDTO> UpdateFridge(Guid id, UpdateFridgeDTO fridge);

        Task DeleteFridgeById(Guid id);

        Task<List<ProductsDTO>> GetFridgeProducts(Guid id);



    }
}
