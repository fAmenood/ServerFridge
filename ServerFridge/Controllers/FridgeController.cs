using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        [HttpGet]   
        public async Task <IActionResult> GetAllFridges()
        {
            var fridges = await
            return Ok();
        }
    }
}
