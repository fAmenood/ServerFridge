using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerFridge.Models
{
    public class FridgeModel
    {
        [Key]
        [Column("ModelId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fridge model name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name model is 60 characters.")]
        public string Name { get; set; }

        [Range(1,2025,ErrorMessage ="Year can't be negative and more 2025")]
        public int Year { get;set; }
    }
}
