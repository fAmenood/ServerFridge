using ServerFridge.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ServerFridge.DTOModels
{
    public class FridgeProductsDTO
    {
     
        public Guid Id { get; set; } = Guid.NewGuid();


        public Guid ProductId { get; set; }



        public Guid FridgeId { get; set; }

        public int Quantity { get; set; }

    }
}
