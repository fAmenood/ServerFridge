using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;
using ServerFridge.Repository;

namespace ServerFridge.Controllers
{
    [Route("api/fridges")]
    [ApiController]
 
    public class FridgeController : ControllerBase
    {
        private readonly IFridgeRepository fridgeRepository;


        public FridgeController(IFridgeRepository _fridgeRepository)
        {
            fridgeRepository = _fridgeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFridges()
        {
            try
            {
                var fridges = await fridgeRepository.GetFridges();
                return Ok(fridges);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
   
        
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetFridgeProducts(Guid id)
        {
            try
            {
                var fridge = await fridgeRepository.GetFridgeProducts(id);

                if (fridge == null)
                {
                    return NotFound($"Fridge with id {id} not found");
                }
                return Ok(fridge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("{id}")]
  
        public async Task<IActionResult> GetFridge(Guid id)
        {
            try
            {
                var fridge = await fridgeRepository.GetFridgeById(id);
                return fridge != null ? Ok(fridge) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddNewFridge(FridgeCreateDTO fridge)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var newFridge = await fridgeRepository.AddFridge(fridge);
                return CreatedAtAction(nameof(GetFridge), new { id = newFridge.Id }, newFridge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFridge(Guid id, UpdateFridgeDTO fridge)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var updatedFridge = await fridgeRepository.UpdateFridge(id, fridge);
                return updatedFridge != null ? Ok(updatedFridge) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFridgeById(Guid id)
        {
            await fridgeRepository.DeleteFridgeById(id);
            return NoContent(); 
        }
    }

}
