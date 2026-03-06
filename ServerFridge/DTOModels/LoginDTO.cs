using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
