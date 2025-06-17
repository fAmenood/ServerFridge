using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerFridge.DataContext;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FridgeController : ControllerBase
    {
        private readonly AppDbContext appDbContext;


        public FridgeController(AppDbContext _appDbContext)
        {
            appDbContext = _appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFridges()
        {

            return Ok(await appDbContext.Fridges.ToListAsync());
        }
        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetFridgeProducts(Guid id)
        {
            var prods = await appDbContext.FridgeProducts.Where(fr=>fr.FridgeId==id).Include(fr=>fr.Products).ToListAsync();
            return Ok(prods);
        }

    }
}
