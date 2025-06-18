using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;
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

                if(fridge == null)
                {
                    return NotFound($"Fridge with id {id} not found");
                }
                return Ok(fridge);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message); 
            }
        }
        [HttpPost]
    }
}
