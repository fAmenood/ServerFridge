using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerFridge.Models
{
    public class FridgeProducts
    {
        [Key]
        [Column("FridgeProductId")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Products))]
        public Guid ProductId { get; set; }
        public Products Products { get; set; }

        [Required]
        [ForeignKey(nameof(Fridge))]
        public Guid FridgeId { get; set; }  
        public Fridge Fridge { get; set; }

        [Required]
        public int Quantity { get; set; }




    }
}
