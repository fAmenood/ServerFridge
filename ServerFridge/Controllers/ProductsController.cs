using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServerFridge.DTOModels;
using ServerFridge.Repository;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;


        public ProductsController(IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await productRepository.GetProducts();
                return Ok(products);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var product = await productRepository.GetProductById(id);

                if (product == null)
                {
                    return NotFound($"Product with {id} is not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductCreateDTO product)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var newProduct = await productRepository.AddProduct(product);
                return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductsDTO product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {


                if (product.Name == null && product.DefaultQuantity == 0)
                {
                    return BadRequest("At least one field must required for updating");
                }
                var updatedProduct = await productRepository.UpdateProduct(id, product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await productRepository.DeleteProduct(id);
            return NoContent();
        }

    }
}
