using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerFridge.Repository;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeModelController : ControllerBase
    {

        private readonly IModelsRepository modelsRepository;


        public FridgeModelController(IModelsRepository _modelsRepository)
        {
            modelsRepository = _modelsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllModels()
        {
            try
            {
                var models = await modelsRepository.GetAllModels();
                return Ok(models);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]

        public async Task<IActionResult> GetModelById(Guid id)
        {
            try
            {
                var model = await modelsRepository.GetModelById(id);
                return model != null ? Ok(model) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
      
}
