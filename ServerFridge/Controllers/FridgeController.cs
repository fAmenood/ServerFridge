using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;
using ServerFridge.Repository;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
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
        [Authorize]
        public async Task<IActionResult> GetFridge(Guid id)
        {
            try
            {
                var fridge = await fridgeRepository.GetFridgeById(id);

                if (fridge == null)
                {
                    return NotFound($"Fridge with {id} is not found");
                }

                return Ok(fridge);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddNewFridge(FridgeDTO fridge)
        {
            try
            {
                if (fridge == null || !ModelState.IsValid)
                {
                    return BadRequest("Fridge doesn't exist or invalid model");
                }
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedFridge = await fridgeRepository.UpdateFridge(id, fridge);
                if (updatedFridge == null) return NotFound();

                return Ok(updatedFridge);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
