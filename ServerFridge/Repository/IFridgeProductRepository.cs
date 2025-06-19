using Microsoft.AspNetCore.Mvc;
using ServerFridge.DTOModels;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public interface IFridgeProductRepository
    {
        Task<List<FridgeProductsDTO>> GetFridgeProducts();

        Task<FridgeProductsDTO> AddProductToFridge(FridgeProductsDTO fridgeProductsDTO);

        Task<FridgeProductsDTO> GetFridgeProductsById(Guid id);

        Task<FridgeProductsDTO> UpdateFridgeProducts(Guid id, UpdateFridgeProductsDTO fridgeProductsDTO);

        Task<bool> DeleteFridgeProducts(Guid id);

        Task<int> ZeroQuantityFridgeProducts();

    }
}
    