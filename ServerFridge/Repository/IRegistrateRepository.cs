using ServerFridge.DTOModels;
using ServerFridge.Models;

namespace ServerFridge.Repository
{
    public interface IRegistrateRepository
    {
        Task<User> SignUp(RegisterDTO registerDTO);
        Task<JWTResponseDTO>Login(LoginDTO loginDTO);
    }
}
