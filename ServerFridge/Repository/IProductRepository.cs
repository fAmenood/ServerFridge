using ServerFridge.DTOModels;

namespace ServerFridge.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductsDTO>> GetProducts();

        Task<ProductsDTO> GetProductById(Guid id);

        Task<ProductsDTO> AddProduct(ProductCreateDTO fridge);

        Task<UpdateProductsDTO> UpdateProduct(Guid id, UpdateProductsDTO fridge);

        Task DeleteProduct(Guid id);   
    }
}
