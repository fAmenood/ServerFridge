//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Identity.Client;
//using Microsoft.IdentityModel.Tokens;
//using ServerFridge.DTOModels;
//using ServerFridge.Repository;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace ServerFridge.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly IRegistrateRepository _registrateRepository;

//        public AuthController(IRegistrateRepository registrateRepository)
//        {
//            _registrateRepository = registrateRepository;
//        }
//        [HttpPost("register")]
//        public async Task<IActionResult> Register(RegisterDTO registerDTO)
//        {
//            try
//            {
//                var user = await _registrateRepository.SignUp(registerDTO);
//                return Ok(new
//                {
//                    user.Id,
//                    user.Email,
//                    user.Role,

//                });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
   
//        [HttpPost("login")]
//        public async Task<IActionResult> Login(LoginDTO loginDTO)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            try
//            {
//                var request = await _registrateRepository.Login(loginDTO);
//                Console.WriteLine($"Generated token: {request.JWTToken}");
//                return Ok(request);
//            }
//            catch (Exception ex)
//            {
//                return Unauthorized(ex.Message);
//            }
//        }
     
//    }
//}

