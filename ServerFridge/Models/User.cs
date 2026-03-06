using System.ComponentModel.DataAnnotations;

namespace ServerFridge.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]  
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public DateTime TimeCreate { get; set; } = DateTime.UtcNow;
    }
}
