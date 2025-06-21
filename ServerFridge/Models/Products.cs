using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerFridge.Models
{
    public class Products
    {
        [Key]
        [Column("ProductId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Default quantity must be at least 1")]
        public int DefaultQuantity {  get; set; }


        [Column("Image Path")]
        public string? ImagePath { get; set; }


    }
}
