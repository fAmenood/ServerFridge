using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerFridge.DataContext;
using ServerFridge.DTOModels;
using ServerFridge.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServerFridge.Repository
{
    public class RegistrateRepository : IRegistrateRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public RegistrateRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        public async Task<User> SignUp(RegisterDTO registerDTO)
        {
            if (await _appDbContext.Users.AnyAsync(user => user.Email == registerDTO.Email))
            {
                throw new Exception("User with this email already exists");
            }
            if (registerDTO.Role != "Admin" && registerDTO.Role != "User")
            {
                throw new Exception("Role doesn't exist");

            }

            var passwordHash = HashPassword(registerDTO.Password);

            var user = new User
            {
                Email = registerDTO.Email,
                PasswordHash = passwordHash,
                Role = registerDTO.Role,
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            return user;
        }



        public async Task<JWTResponseDTO> Login(LoginDTO loginDTO)
        {


            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Email == loginDTO.Email);
            if (user == null || !CheckPassword(loginDTO.Password, user.PasswordHash))
            {
                throw new Exception("Invalid password or user doesn't exist");
            }
            if (user.Role != "Admin" && user.Role != "User")
            {
                throw new Exception("User has invalid role");
            }

            return CreateToken(user);
        }
        public string HashPassword(string password)
        {
            var hash = SHA256.Create();
            var hashedToUtf8 = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedToUtf8);
        }
        public bool CheckPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
        public JWTResponseDTO CreateToken(User user)
        {
            var secretKey = _configuration["JWTToken:SecretKey"];
            var issuers = _configuration["JWTToken:Issuer"];
            var audiences = _configuration["JWTToken:Audience"];



            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            var claim = new[]
            {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("role", user.Role)


            };

            var timeToSign = DateTime.UtcNow.AddHours(12);

            var token = new JwtSecurityToken
            (
                issuer: issuers,
                audience: audiences,
                claims: claim,
                expires: timeToSign,
                signingCredentials: creds



            );

            Console.WriteLine($"SecretKey: {secretKey}");
            Console.WriteLine($"SecretKey bytes: {BitConverter.ToString(Encoding.UTF8.GetBytes(secretKey))}");

            return new JWTResponseDTO
            {
                JWTToken = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id,
                Role = user.Role,
                timeToSign = timeToSign,
            };
        }
    }
}
