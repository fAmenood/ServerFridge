using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ServerFridge.DTOModels
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Default quantity must be non-negative")]
        public int DefaultQuantity { get; set; }

    }
}
