using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerFridge.DTOModels;
using ServerFridge.Models;
using ServerFridge.Repository;

namespace ServerFridge.Controllers
{
    [Route("api/fridgeProducts")]
    [ApiController]
    public class FridgeProductsController : ControllerBase
    {
        private readonly IFridgeProductRepository _fridgeProductRep;

        public FridgeProductsController(IFridgeProductRepository fridgeProductRep)
        {
            _fridgeProductRep = fridgeProductRep;
        }
        [HttpGet]
        public async Task<IActionResult> GetFridgeProducts()
        {
            try
            {
                var frProds = await _fridgeProductRep.GetFridgeProducts();
                return Ok(frProds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
       
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFridgeProductsById(Guid id)
        {
            var frProd = await _fridgeProductRep.GetFridgeProductsById(id);
            return frProd != null ? Ok(frProd) : NotFound();
        }
        [HttpPost]
        
        public async Task<IActionResult> AddFridgeProducts(FridgeProductsDTO fridgeProductsDTO)
        {
       
                if(!ModelState.IsValid)
                {
                    return BadRequest("Something not correct");
                }
              try
              {
                var frProds = await _fridgeProductRep.AddProductToFridge(fridgeProductsDTO);
                return CreatedAtAction(nameof(GetFridgeProductsById), new { id = frProds.Id }, frProds);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFridgeProducts(Guid id, UpdateFridgeProductsDTO fridgeProductsDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (fridgeProductsDTO.Quantity == null && fridgeProductsDTO.ProductId == null && fridgeProductsDTO.FridgeId == null)
                return BadRequest("At least one field must required for updating");
            var updateFrProds=await _fridgeProductRep.UpdateFridgeProducts(id,fridgeProductsDTO);
            return updateFrProds !=null? Ok(updateFrProds) : NotFound();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridgeProducts(Guid id)
        {
            var del = await _fridgeProductRep.DeleteFridgeProducts(id);
            return del ? NoContent() : NotFound();
        }
        [HttpPost("procedure")]
        public async Task<IActionResult> ZeroQuantityFridgeProds()
        {
            try
            {
                var count = await _fridgeProductRep.ZeroQuantityFridgeProducts();
                return Ok
                (
                    new { Message =$"Amount of products with zero quantity: {count} ",
                    Counter = count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
