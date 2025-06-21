using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class ProductsDTO
    {
        public Guid Id { get; set; } 

        public string? Name { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Default quantity must be at least 1")]
        public int DefaultQuantity { get; set; }
    }
}
