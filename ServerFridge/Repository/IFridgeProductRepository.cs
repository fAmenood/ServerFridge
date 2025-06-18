using Microsoft.AspNetCore.Mvc;
using ServerFridge.DTOModels;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public interface IFridgeProductRepository
    {
        Task<List<FridgeProductsDTO>> GetFridgeProducts();

        Task<FridgeProductsDTO> AddProductToFridge(FridgeProductsDTO fridgeProductsDTO);


    }
}
    