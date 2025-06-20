using ServerFridge.DTOModels;

namespace ServerFridge.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductsDTO>> GetProducts();

        Task<ProductsDTO> GetProductById(Guid id);

        Task<ProductsDTO> AddProduct(FridgeDTO fridge);

        Task<ProductsDTO> UpdateProduct(Guid id, UpdateFridgeDTO fridge);
    }
}
