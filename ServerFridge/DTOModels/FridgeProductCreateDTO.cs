using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ServerFridge.DTOModels
{
    public class FridgeProductCreateDTO
    {
        [Required(ErrorMessage = "Fridge ID is required")]
        public Guid FridgeId { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        public Guid ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative")]
        public int Quantity { get; set; }
    }
}
