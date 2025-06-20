using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class FridgeModelsDTO
    {

        [Required(ErrorMessage = "Fridge model name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name model is 60 characters.")]
        public string Name { get; set; }

        public int Year { get; set; }
    }
}
