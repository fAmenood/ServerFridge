using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerFridge.Models
{
    public class Fridge
    {
        [Key]
        [Column("Fridge Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fridge name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        [MaxLength(60, ErrorMessage = "Maximum length for the Owner name is 60 characters.")]
        public string OwnerName { get; set; }

      
        [ForeignKey(nameof(FridgeModel))]
        public Guid ModelId { get; set; }
        public FridgeModel FridgeModel { get; set; }    
    
    }
}
