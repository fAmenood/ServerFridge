using Microsoft.AspNetCore.Mvc;
using ServerFridge.DTOModels;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public interface IModelsRepository
    {
        Task<List<FridgeModelDTO>> GetAllModels();
        Task<FridgeModelsDTO> GetModelById(Guid id);
    }
}
