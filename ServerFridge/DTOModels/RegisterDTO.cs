using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100,MinimumLength=8)]
        public string Password { get; set; }

        [Required] 
        public string Role { get; set; }
    }
}
