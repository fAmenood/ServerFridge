using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class FridgeCreateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Owner name cannot be longer than 100 characters")]
        [Required(ErrorMessage = "Name is required")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Model ID is required")]
        public Guid ModelId { get; set; }
    }
}
