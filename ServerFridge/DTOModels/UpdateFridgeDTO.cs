using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class UpdateFridgeDTO
    {
        [StringLength(60, ErrorMessage = "Name length can't be more than 60 characters")]
        public string? Name { get; set; }

        [StringLength(60, ErrorMessage = "Owner name length can't be more than 60 characters")]
        public string? OwnerName { get; set; }

        public Guid? ModelId { get; set; }
    }
}
