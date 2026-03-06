using ServerFridge.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class FridgeDTO
    {
        
        public Guid Id { get; set; } 

        
        public string Name { get; set; }

        
        public string OwnerName { get; set; }

        public Guid ModelId { get; set; }


    

    }
}
