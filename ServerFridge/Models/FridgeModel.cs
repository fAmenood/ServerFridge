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

        public int Year { get;set; }
    }
}
