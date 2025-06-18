using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerFridge.DTOModels;
using ServerFridge.Models;
using ServerFridge.Repository;

namespace ServerFridge.Controllers
{
    [Route("api/[controller]")]
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
                var frProds= await _fridgeProductRep.GetFridgeProducts();
                return Ok(frProds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
